using NUnit.Framework;
using RepositoryMapByCode.Models;
using RepositoryMapByCode.Repositories;
using Shouldly;
using System;
using System.Collections.Generic;

namespace Tests.MappingByCode
{
    public class CatStoreMbcTests
    {
        private IRepository<CatStore> _catStoreRepository;
        private IRepository<Cat> _catRepository;

        [SetUp]
        public void SetUp()
        {
            NHibernateHelper.ResetSchema();
            _catStoreRepository = new Repository<CatStore>();
            _catRepository = new Repository<Cat>();
        }

        [Test]
        public void CollectionTest()
        {
            var catStore = new CatStore
            {
                Name = "Happy Cats"
            };
            var cat1 = new Cat
            {
                Name = "Tom 1",
                Weight = 1.0f,
                CatStore = catStore,
            };
            var cat2 = new Cat
            {
                Name = "Tom 2",
                Weight = 1.0f,
                CatStore = catStore,
            };
            catStore.Cats.Add(cat1);
            catStore.Cats.Add(cat2);
            _catStoreRepository.Add(catStore);

            cat1.Id.ShouldNotBeEmpty();
            cat2.Id.ShouldNotBeEmpty();
            catStore.Id.ShouldNotBe(Guid.Empty);

            catStore.Cats.Remove(cat1);
            _catStoreRepository.Update(catStore);

            _catRepository.Get(cat1.Id).ShouldBeNull();
        }

        [Test]
        public void FetchTest()
        {
            var catStore = new CatStore
            {
                Name = "Happy Cats"
            };
            var cat1 = new Cat
            {
                Name = "Tom 1",
                Weight = 1.0f,
                CatStore = catStore,
            };
            var cat2 = new Cat
            {
                Name = "Tom 2",
                Weight = 1.0f,
                CatStore = catStore,
            };
            catStore.Cats.Add(cat1);
            catStore.Cats.Add(cat2);
            _catStoreRepository.Add(catStore);

            using var session = NHibernateHelper.OpenSession();
            var x = session.Get<CatStore>(catStore.Id);
            // Two kinds of lazy loading:
            // - lazy=false -> still having 2 quyery (first get CatStore, second to get Cats), but they're executed when calling Get().
            // - fetch=join -> using `join` therefore have only 1 query to get.

            Console.WriteLine(x.Cats);
        }

        [Test]
        public void SetOfStringTest()
        {
            var catStore = new CatStore
            {
                Name = "Happy Cats",
                PhoneNumbers = new SortedSet<string> { "111 111 111", "222 222 222" },
            };
            _catStoreRepository.Add(catStore);
        }
    }
}
