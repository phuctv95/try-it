using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using QuickStart.Repository;
using System;

/// <summary>
/// Single table inheritance.
/// </summary>
namespace QuickStart.Domain
{
    public class Person
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
    public class NationalCitizen : Person
    {
        public virtual string IdCard { get; set; }
    }
    public class ForeignCitizen : Person
    {
        public virtual string Country { get; set; }
    }

    public class PersonMapping : ClassMapping<Person>
    {
        public PersonMapping()
        {
            Table("Person");
            Lazy(true);
            Discriminator(x =>
            {
                x.Column("Class");
                x.NotNullable(true);
            });
            Id(x => x.Id, x =>
            {
                x.Column("Id");
                x.Generator(Generators.HighLow);
            });
            Property(x => x.Name, x =>
            {
                x.Column("Name");
                x.NotNullable(true);
                x.Length(100);
            });
        }
    }
    public class NationalCitizenMapping : SubclassMapping<NationalCitizen>
    {
        public NationalCitizenMapping()
        {
            DiscriminatorValue("NC");
            Lazy(true);

            Property(x => x.IdCard, x =>
            {
                x.Column("IdCard");
                x.Length(20);
            });
        }
    }
    public class ForeignCitizenMapping : SubclassMapping<ForeignCitizen>
    {
        public ForeignCitizenMapping()
        {
            DiscriminatorValue("FC");
            Lazy(true);

            Property(x => x.Country, x =>
            {
                x.Column("Country");
                x.Length(20);
            });
        }
    }
    public static class PersonDemo
    {
        public static void Demo()
        {
            var person = new NationalCitizen
            {
                Name = "Name1",
                IdCard = "001",
            };
            new Repository<NationalCitizen>().Add(person);
            Console.WriteLine(person.Id);
        }
    }
}
