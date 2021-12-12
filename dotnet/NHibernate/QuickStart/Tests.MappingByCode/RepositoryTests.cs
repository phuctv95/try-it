using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NUnit.Framework;
using RepositoryMapByCode.Mappings;
using RepositoryMapByCode.Models;
using RepositoryMapByCode.Repositories;

namespace Tests.MappingByCode
{
    public class RepositoryTests
    {
        private ISessionFactory _sessionFactory;
        private Configuration _configuration;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var mapper = new ModelMapper();
            mapper.AddMapping<StreetMap>();

            _configuration = new Configuration();
            _configuration.Configure();
            _configuration.AddMapping(mapper.CompileMappingFor(new[] { typeof(Street) }));
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        [SetUp]
        public void SetUp()
        {
            NHibernateHelper.ResetSchema();
        }

        [Test]
        public void CanAdd()
        {
            var street = new Street
            {
                Name = "Street 1"
            };
            var repository = new Repository<Street>();

            repository.Add(street);

            using var session = _sessionFactory.OpenSession();
            var actual = session.Get<Street>(street.Id);
            Assert.IsNotNull(actual);
            Assert.AreNotSame(street, actual);
            Assert.AreEqual(street.Id, actual.Id);
            Assert.AreEqual(street.Name, actual.Name);
        }
    }
}
