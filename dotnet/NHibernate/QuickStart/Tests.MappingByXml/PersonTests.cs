using NUnit.Framework;
using Repository.Models;
using Repository.Repositories;
using System;
using System.Collections.Generic;

namespace Tests.MappingByXml
{
    public class PersonTests
    {

        [SetUp]
        public void SetUp()
        {
            NHibernateHelper.ResetSchema();
        }

        [Test]
        public void DynamicComponentTest()
        {
            var repo = new Repository<Person>();
            repo.Add(new Person
            {
                Attributes = new Dictionary<string, object>
                {
                    { "FirstName", "Dave" },
                    { "LastName", "Gore" },
                    { "BirthDate", DateTime.Now },
                },
                Employee = new Employee
                {
                    Role = "Manager",
                }
            });
        }

        [Test]
        public void OneToOneTest()
        {
            var personRepository = new Repository<Person>();
            var employeeRepository = new Repository<Employee>();
            var person = new Person
            {
                Attributes = new Dictionary<string, object>
                {
                    { "FirstName", "Dave" },
                },
            };
            var employee = new Employee
            { 
                Role = "Manager",
                Person = person,
            };
            person.Employee = employee;

            personRepository.Add(person);
            // Following Add() produces the same result.
            //employeeRepository.Add(employee);

            using (var session = NHibernateHelper.OpenSession())
            {
                Assert.AreEqual(
                    person.Id,
                    session.Get<Employee>(employee.Id).Person.Id);
                Assert.AreEqual(
                    employee.Id,
                    session.Get<Person>(person.Id).Employee.Id);
            }
        }
    }
}
