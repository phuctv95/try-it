using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Repository.Domain;
using Repository.Repositories;
using System.Linq;

namespace Tests
{
    [TestFixture]
    class ProductRepositoryTests
    {
        private ISessionFactory _sessionFactory;
        private Configuration _configuration;
        private readonly Product[] _products = new[]
        {
            new Product {Name = "Melon", Category = "Fruits"},
            new Product {Name = "Pear", Category = "Fruits"},
            new Product {Name = "Milk", Category = "Beverages"},
            new Product {Name = "Coca Cola", Category = "Beverages"},
            new Product {Name = "Pepsi Cola", Category = "Beverages"},
        };

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _configuration = new Configuration();
            _configuration.Configure();
            _configuration.AddAssembly(typeof(Product).Assembly);

            // This is an expensive process so it's in OneTimeSetUp.
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        [SetUp]
        public void SetUp()
        {
            new SchemaExport(_configuration).Execute(false, true, false);
            CreateInitialData();
        }

        [Test]
        public void CanAddNewProduct()
        {
            var product = new Product
            {
                Name = "Apple",
                Category = "Fruits",
            };
            var repository = new ProductRepository();

            repository.Add(product);

            using var session = _sessionFactory.OpenSession();
            var actual = session.Get<Product>(product.Id);
            Assert.IsNotNull(actual);
            Assert.AreNotSame(product, actual);
            Assert.AreEqual(product.Name, actual.Name);
            Assert.AreEqual(product.Category, actual.Category);
        }

        [Test]
        public void CanUpdateExistingProduct()
        {
            var product = _products[0];
            product.Name = "Yellow Pear";
            var repository = new ProductRepository();

            repository.Update(product);

            using var session = _sessionFactory.OpenSession();
            var actual = session.Get<Product>(product.Id);
            Assert.AreEqual(product.Name, actual.Name);
        }

        [Test]
        public void CaRemoveExistingProduct()
        {
            var product = _products[0];
            var repository = new ProductRepository();

            repository.Remove(product);

            using var session = _sessionFactory.OpenSession();
            var actual = session.Get<Product>(product.Id);
            Assert.IsNull(actual);
        }

        [Test]
        public void CanGetExistingProductById()
        {
            var repository = new ProductRepository();

            var actual = repository.GetById(_products[1].Id);

            Assert.IsNotNull(actual);
            Assert.AreNotSame(_products[1], actual);
            Assert.AreEqual(_products[1].Id, actual.Id);
            Assert.AreEqual(_products[1].Name, actual.Name);
        }

        [Test]
        public void CanGetExistingProductByName()
        {
            var repository = new ProductRepository();

            var actual = repository.GetByName(_products[1].Name);

            Assert.IsNotNull(actual);
            Assert.AreNotSame(_products[1], actual);
            Assert.AreEqual(_products[1].Id, actual.Id);
            Assert.AreEqual(_products[1].Name, actual.Name);
        }

        [Test]
        public void CanGetExistingProductByCategory()
        {
            var repository = new ProductRepository();

            var actual = repository.GetByCategory(_products[1].Category);

            Assert.AreEqual(2, actual.Count);
            Assert.AreNotSame(_products[1], actual);
            Assert.IsTrue(actual.Any(p => p.Id == _products[0].Id));
            Assert.IsTrue(actual.Any(p => p.Id == _products[1].Id));
        }

        private void CreateInitialData()
        {
            using var session = _sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            foreach (var product in _products)
            {
                session.Save(product);
            }
            transaction.Commit();
        }
    }
}
