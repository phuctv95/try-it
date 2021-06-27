using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using QuickStart.Repository;
using System;
/// <summary>
/// Table per class.
/// </summary>
namespace QuickStart.Domain
{
    public class Sport
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
    public class SportFootball : Sport
    {
        public virtual int Team1Rating { get; set; }
        public virtual int Team2Rating { get; set; }
    }
    public class SportChess : Sport
    {
        public virtual string Player1Country { get; set; }
        public virtual string Player2Country { get; set; }
    }

    public class SportMapping : ClassMapping<Sport>
    {
        public SportMapping()
        {
            Table("Sport");
            Lazy(true);
            Id(x => x.Id, x =>
            {
                x.Column("Id");
                x.Generator(Generators.HighLow);
            });
            Property(x => x.Name, x =>
            {
                x.Column("Name");
                x.NotNullable(true);
            });
        }
    }
    public class SportFootballMapping : JoinedSubclassMapping<SportFootball>
    {
        public SportFootballMapping()
        {
            Table("SportFootball");
            Lazy(true);
            Key(x =>
            {
                x.Column("Id");
                x.NotNullable(true);
            });
            Property(x => x.Team1Rating, x =>
            {
                x.Column("Team1Rating");
                x.NotNullable(true);
            });
            Property(x => x.Team2Rating, x =>
            {
                x.Column("Team2Rating");
                x.NotNullable(true);
            });
        }
    }
    public class SportChessMapping : JoinedSubclassMapping<SportChess>
    {
        public SportChessMapping()
        {
            Table("SportChess");
            Lazy(true);
            Key(x =>
            {
                x.Column("Id");
                x.NotNullable(true);
            });
            Property(x => x.Player1Country, x =>
            {
                x.Column("Player1Country");
                x.NotNullable(true);
            });
            Property(x => x.Player2Country, x =>
            {
                x.Column("Player2Country");
                x.NotNullable(true);
            });
        }
    }

    public static class SportDemo
    {
        public static void Demo()
        {
            var footballMatch = new SportFootball
            {
                Name = "Football",
                Team1Rating = 4,
                Team2Rating = 3,
            };
            new Repository<SportFootball>().Add(footballMatch);
            Console.WriteLine(footballMatch.Id);
        }
    }
}
