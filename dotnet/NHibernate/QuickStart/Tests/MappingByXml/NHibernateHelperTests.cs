using NUnit.Framework;
using Repository.Models;
using Repository.Repositories;

namespace Tests.MappingByXml
{
    public class NHibernateHelperTests
    {
        [Test]
        public void CanGenerateSchema()
        {
            Assert.DoesNotThrow(NHibernateHelper.ResetSchema);
        }
    }
}