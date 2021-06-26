using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TryEntityFramework.Models;

namespace TryEntityFramework.Repository
{
    public class MyDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Motorbike> Motorbikes { get; set; }

        public MyDbContext() : base("name=MyDbContext") {}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
