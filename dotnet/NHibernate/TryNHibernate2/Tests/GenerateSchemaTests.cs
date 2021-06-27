using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Repository.Domain;

namespace Tests
{
    public class GenerateSchemaTests
    {
        [Test]
        public void CanGenerateSchema()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Product).Assembly);

            new SchemaExport(cfg).Execute(false, true, false);

            Assert.Pass();
        }
    }
}
