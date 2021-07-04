using NUnit.Framework;
using Repository.Models;
using Repository.Repositories;

namespace Tests.MappingByXml
{
    public class NHibernateTests
    {
        [SetUp]
        public void SetUp()
        {
            NHibernateHelper.ResetSchema();
        }

        [Test]
        public void WhenAddAndThenGetInSameSession_BothAreSame()
        {
            using var session = NHibernateHelper.OpenSession();
            using var transaction = session.BeginTransaction();
            var cat = new Cat { Name = "Tom", Sex = 'm', Weight = 1.0f };
            session.Save(cat);

            Assert.AreSame(cat, session.Get<Cat>(cat.Id));
            transaction.Commit();

            Assert.AreSame(cat, session.Get<Cat>(cat.Id));
        }

        [Test]
        public void WhenAddAndThenGetInAnotherSession_TheyAreNotTheSameBecauseItDetached()
        {
            var cat = new Cat { Name = "Tom", Sex = 'm', Weight = 1.0f };
            using (var session = NHibernateHelper.OpenSession())
            {
                using var transaction = session.BeginTransaction();
                session.Save(cat);
                transaction.Commit();
            }

            using (var session = NHibernateHelper.OpenSession())
            {
                Assert.AreNotSame(cat, session.Get<Cat>(cat.Id));
            }
        }
    }
}
