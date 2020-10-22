using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TryWpf.Model;

namespace TryWpf.ViewModel
{
    public class StudentsViewModel
    {
        public StudentsViewModel()
        {
            LoadStudents();
            DeleteCommand = new MyCommand(Delete, CanDelete);
        }

        public MyCommand DeleteCommand { get; set; }
        public ObservableCollection<Student> Students
        {
            get;
            set;
        }
        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get
            {
                return _selectedStudent;
            }

            set
            {
                _selectedStudent = value;
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public void LoadStudents()
        {
            ObservableCollection<Student> students = new ObservableCollection<Student>();

            students.Add(new Student { FirstName = "Mark", LastName = "Allain" });
            students.Add(new Student { FirstName = "Allen", LastName = "Brown" });
            students.Add(new Student { FirstName = "Linda", LastName = "Hamerski" });

            Students = students;
        }
        private void Delete() => Students.Remove(SelectedStudent);
        private bool CanDelete() => SelectedStudent != null;
    }
}
