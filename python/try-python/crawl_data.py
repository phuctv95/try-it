import csv, datetime, re, subprocess, sys
from bs4 import BeautifulSoup

class Student:
    pass

def search_reg_return_first_group(pattern: str, s: str, defaultValue: str):
    result = re.search(pattern, s)
    if result:
        return result.group(1)
    else:
        return defaultValue

def estimate_remaining(number_of_done, total, bunch_size, bunch_time: datetime.timedelta):
    if bunch_time == None:
        return 'Estimating...'

    number_of_remaining = total - number_of_done
    number_of_bunch_remaining = number_of_remaining / bunch_size
    return f'{str(bunch_time * number_of_bunch_remaining)} remaining'

start_id_number = 2000001
end_id_number = 2074718
bunch_size = 10
bunch_time = None
estimate_begin = None
item_in_bunch = 0

# with open('data.csv', mode='w', encoding='utf-8') as csv_file:
with open('data.csv', mode='a', encoding='utf-8') as csv_file:
    # csv_file.write(
    #     'id_number,full_name,dob,math,literature,physical,chemistry,'
    #     + 'biological,social_science,natural_science,english,history,'
    #     + 'geography,civic_education'
    #     + '\n')

    for id_number in range(start_id_number, end_id_number + 1):
        if item_in_bunch == 0:
            estimate_begin = datetime.datetime.now()
        
        result = subprocess \
            .check_output(f'curl -s -F "SoBaoDanh={id_number:08d}" http://diemthi.hcm.edu.vn/Home/Show') \
            .decode(sys.stdout.encoding)
        soup = BeautifulSoup(result, 'html.parser')
        full_name = soup.select_one('table tr:nth-child(2) > td')
        if full_name == None:
            continue

        student = Student()
        student.id_number = f'{id_number:08d}'
        student.full_name = full_name.text.strip()
        student.dob = soup.select_one('table tr:nth-child(2) > td:nth-child(2)').text.strip() # dd/mm/yyyy
        scores = soup.select_one('table tr:nth-child(2) > td:nth-child(3)').text.strip()
        student.math = search_reg_return_first_group('Toán:\s.+?(\d\.\d{2})', scores, -1)
        student.literature = search_reg_return_first_group('Ngữ văn:\s.+?(\d\.\d{2})', scores, -1)
        student.physical = search_reg_return_first_group('Vật lí:\s.+?(\d\.\d{2})', scores, -1)
        student.chemistry = search_reg_return_first_group('Hóa học:\s.+?(\d\.\d{2})', scores, -1)
        student.biological = search_reg_return_first_group('Sinh học:\s.+?(\d\.\d{2})', scores, -1)
        student.social_science = search_reg_return_first_group('KHXH:\s.+?(\d\.\d{2})', scores, -1)
        student.natural_science = search_reg_return_first_group('KHTN:\s.+?(\d\.\d{2})', scores, -1)
        student.english = search_reg_return_first_group('Tiếng Anh:\s.+?(\d\.\d{2})', scores, -1)
        student.history = search_reg_return_first_group('Lịch sử:\s.+?(\d\.\d{2})', scores, -1)
        student.geography = search_reg_return_first_group('Địa lí:\s.+?(\d\.\d{2})', scores, -1)
        student.civic_education = search_reg_return_first_group('GDCD:\s.+?(\d\.\d{2})', scores, -1)

        csv_file.write(
            f'{student.id_number},{student.full_name},{student.dob},{student.math},'
            + f'{student.literature},{student.physical},{student.chemistry},{student.biological},'
            + f'{student.social_science},{student.natural_science},{student.english},'
            + f'{student.history},{student.geography},{student.civic_education}'
            + '\n')
        
        item_in_bunch = item_in_bunch + 1
        if item_in_bunch == 10:
            bunch_time = datetime.datetime.now() - estimate_begin
            item_in_bunch = 0

        print(f'Retrieved {student.id_number}: {student.full_name:<25} ({end_id_number - id_number:,} left - '
            f'{estimate_remaining(id_number, end_id_number, bunch_size, bunch_time)})')

print('Finished.')
