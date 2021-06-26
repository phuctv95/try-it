using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using TryEntityFramework.Models;
using TryEntityFramework.Repository;

namespace TryEntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<int> list = new List<int>();
            foreach (var item in list)
            {
                list.Add(1);
            }
        }

        private static void Demo2()
        {
            using (var db = new MyDbContext())
            {
                var person = db.Persons.FirstOrDefault(p => p.Name == "Cris");
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Persons.Remove(person);
                db.SaveChanges();
                db.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        private static void Measure1()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            using (var db = new MyDbContext())
            {
                var person = db.Persons
                    .Include(p => p.Motorbikes)
                    .FirstOrDefault(p => p.Name == "Leo");
                Console.WriteLine(person.Motorbikes == null ? "null" : "not null");
                Console.WriteLine(person.Motorbikes?.Count.ToString());
            }

            Console.WriteLine($"Using Include(): {watch.Elapsed}");
        }

        private static void Measure2()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            using (var db = new MyDbContext())
            {
                var person = db.Persons
                    .FirstOrDefault(p => p.Name == "Leo");
                var tmp = db.Motorbikes.Where(m => m.Owner.Name == "Leo").ToList();
                Console.WriteLine(person.Motorbikes == null ? "null" : "not null");
                Console.WriteLine(person.Motorbikes?.Count.ToString());
            }

            Console.WriteLine($"Not use Include(): {watch.Elapsed}");
        }

        private static void Demo1()
        {
            using (var db = new MyDbContext())
            {
                var person = new Person
                {
                    Name = "Leo",
                    Motorbikes = new List<Motorbike>(),
                };
                var motorbike1 = new Motorbike
                {
                    Brand = "Honda",
                    ReleaseYear = 2018,
                    Owner = person,
                };
                var motorbike2 = new Motorbike
                {
                    Brand = "Yamaha",
                    ReleaseYear = 2018,
                    Owner = person,
                };
                person.Motorbikes.Add(motorbike1);
                person.Motorbikes.Add(motorbike2);
                db.Persons.Add(person);
                db.SaveChanges();
            }
        }

        private static void Demo3()
        {
            using (var db = new MyDbContext())
            {
                var person = new Person
                {
                    Name = "Leo",
                    Motorbikes = new List<Motorbike>(),
                };

                var random = new Random();
                var motorbikes = Enumerable
                    .Range(1, 40000)
                    .Select(i => new Motorbike
                    {
                        Brand = $"Honda {i}",
                        ReleaseYear = random.Next(2005, 2021),
                        Owner = person,
                    })
                    .ToList();
                person.Motorbikes.AddRange(motorbikes);

                db.Persons.Add(person);
                db.SaveChanges();
            }
        }
    }
}
