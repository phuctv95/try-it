import csv
import matplotlib.pyplot as plt
import pandas as pd

df = pd.read_csv('data.csv')
print(len(df[df['geography'] >= 0].index))


subjects = [
    'math', 'literature', 'physical', 'chemistry', 'biological',
    'social_science', 'natural_science', 'english', 'history',
    'geography', 'civic_education']
subject_titles = [
    'Toán', 'Văn', 'Vật Lý', 'Hóa học', 'Sinh học',
    'KHTN', 'KHXH', 'Tiếng Anh', 'Lịch sử',
    'Địa lý', 'GDCD'
]
count_per_subject = [len(df[df[subject] >= 0].index) for subject in subjects]
plt.bar(subject_titles, count_per_subject)
plt.title('Count number of student by subjects')
plt.xlabel('Subjects')
plt.ylabel('Number of students')
for i, v in enumerate(count_per_subject):
    plt.text(i, v + 100, str(v), fontsize=7, ha='center')
plt.show()
