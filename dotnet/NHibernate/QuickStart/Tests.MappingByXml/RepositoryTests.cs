using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;
using Repository.Models;
using Repository.Repositories;

namespace Tests.MappingByXml
{
    public class RepositoryTests
    {
        private ISessionFactory _sessionFactory;
        private Configuration _configuration;
        private readonly Cat[] _cats = new[]
        {
            new Cat { Name = "Cat 1", Sex = 'f', Weight = 1.0f },
            new Cat { Name = "Cat 2", Sex = 'f', Weight = 1.1f },
            new Cat { Name = "Cat 3", Sex = 'm', Weight = 1.4f },
            new Cat { Name = "Cat 4", Sex = 'f', Weight = 1.2f },
            new Cat { Name = "Cat 5", Sex = 'm', Weight = 1.1f },
        };

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _configuration = new Configuration();
            _configuration.Configure();
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        [SetUp]
        public void SetUp()
        {
            NHibernateHelper.ResetSchema();
            CreateInitialData();
        }

        [Test]
        public void CanAdd()
        {
            var cat = new Cat
            {
                Name = "Tom",
                Sex = 'm',
                Weight = 1.0f
            };
            var repository = new Repository<Cat>();

            repository.Add(cat);

            using var session = _sessionFactory.OpenSession();
            var actual = session.Get<Cat>(cat.Id);
            Assert.IsNotNull(actual);
            Assert.AreNotSame(cat, actual);
            Assert.AreEqual(cat.Id, actual.Id);
            Assert.AreEqual(cat.Name, actual.Name);
        }

        [Test]
        public void CanUpdate()
        {
            var product = _cats[0];
            product.Name = "Yellow Pear";
            var repository = new Repository<Cat>();

            repository.Update(product);

            using var session = _sessionFactory.OpenSession();
            var actual = session.Get<Cat>(product.Id);
            Assert.AreEqual(product.Name, actual.Name);
        }

        [Test]
        public void CaRemove()
        {
            var product = _cats[0];
            var repository = new Repository<Cat>();

            repository.Remove(product);

            using var session = _sessionFactory.OpenSession();
            var actual = session.Get<Cat>(product.Id);
            Assert.IsNull(actual);
        }

        [Test]
        public void CanGet()
        {
            var repository = new Repository<Cat>();

            var actual = repository.Get(_cats[1].Id);

            Assert.IsNotNull(actual);
            Assert.AreNotSame(_cats[1], actual);
            Assert.AreEqual(_cats[1].Id, actual.Id);
            Assert.AreEqual(_cats[1].Name, actual.Name);
        }

        private void CreateInitialData()
        {
            using var session = _sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            foreach (var product in _cats)
            {
                session.Save(product);
            }
            transaction.Commit();
        }
    }
}
