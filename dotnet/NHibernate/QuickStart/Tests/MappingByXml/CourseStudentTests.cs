using NUnit.Framework;
using Repository.Models;
using Repository.Repositories;
using Shouldly;
using System;
using System.Collections.Generic;

namespace Tests.MappingByCode
{
    public class CourseStudentTests
    {
        private IRepository<Course> _courseRepository;
        private IRepository<Student> _studentRepository;

        [SetUp]
        public void SetUp()
        {
            NHibernateHelper.ResetSchema();
            _courseRepository = new Repository<Course>();
            _studentRepository = new Repository<Student>();
        }

        [Test]
        public void ManyToManyTest()
        {
            var course1 = new Course
            {
                Name = "Course 1"
            };
            var course2 = new Course
            {
                Name = "Course 2"
            };
            var student1 = new Student
            {
                Name = "Student 1"
            };
            var student2 = new Student
            {
                Name = "Student 2"
            };

            course1.Students.Add(student1);
            course1.Students.Add(student2);
            course2.Students.Add(student1);

            _studentRepository.Add(student1);
            _studentRepository.Add(student2);
            _courseRepository.Add(course1);
            _courseRepository.Add(course2);
        }

        [Test]
        public void InverseTest()
        {
            var course1 = new Course
            {
                Name = "Course 1"
            };
            var student1 = new Student
            {
                Name = "Student 1"
            };
            _studentRepository.Add(student1);
            _courseRepository.Add(course1);

            course1.Students.Add(student1);
            student1.Courses.Add(course1);

            _studentRepository.Update(student1);
            // Because set inverse to Student, so update responsible is belong to Course.
            using (var session = NHibernateHelper.OpenSession())
            {
                session.Get<Course>(course1.Id).Students.Count.ShouldBe(0);
            }

            _courseRepository.Update(course1);
            using (var session = NHibernateHelper.OpenSession())
            {
                session.Get<Course>(course1.Id).Students.Count.ShouldBe(1);
            }
        }
    }
}
