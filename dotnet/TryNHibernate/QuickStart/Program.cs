using System;
using System.Linq;

namespace QuickStart
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                //var princess = new Cat
                //{
                //    Name = "Princess",
                //    Sex = 'F',
                //    Weight = 7.4f
                //};

                //session.Save(princess);
                //tx.Commit();

                var females = session
                    .Query<Cat>()
                    .Where(c => c.Sex == 'F')
                    .ToList();
                foreach (var cat in females)
                {
                    Console.WriteLine($"Female Cat: {cat.Name} ({cat.Id})");
                }
            }
        }
    }
}
