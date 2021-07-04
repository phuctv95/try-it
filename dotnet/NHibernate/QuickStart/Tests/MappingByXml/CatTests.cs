using NUnit.Framework;
using Repository.Models;
using Repository.Repositories;
using Shouldly;

namespace Tests.MappingByXml
{
    public class CatTests
    {
        [SetUp]
        public void SetUp()
        {
            NHibernateHelper.ResetSchema();
        }

        [Test]
        public void VersionTest()
        {
            var repo = new Repository<Cat>();
            var cat = new Cat
            {
                Name = "Tom",
                Weight = 1.0f,
            };
            repo.Add(cat);
            var oldVersion = cat.Version;
            
            cat.Weight = 1.2f;
            repo.Update(cat);

            cat.Version.ShouldNotBe(oldVersion);
        }
    }
}
