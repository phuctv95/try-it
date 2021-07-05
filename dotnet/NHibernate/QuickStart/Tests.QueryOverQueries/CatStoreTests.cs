using NHibernate.Criterion;
using NHibernate.Transform;
using NUnit.Framework;
using RepositoryMapByCode.Models;
using RepositoryMapByCode.Repositories;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.QueryOverQueries
{
    public class CatStoreTests
    {
        private const char Male = 'm';
        private const char Female = 'f';

        private readonly Random _random = new Random();
        private readonly List<Cat> _cats = new List<Cat>();
        private readonly List<CatStore> _catStores = new List<CatStore>();

        [SetUp]
        public void Setup()
        {
            NHibernateHelper.ResetSchema();
            PrepareDataForEachTest();
        }

        [Test]
        public void GettingStartedTest()
        {
            using var session = NHibernateHelper.OpenSession();
            var actual = session.QueryOver<Cat>()
                .Where(c => c.Sex == Male)
                .List();
            var expected = _cats.Where(c => c.Sex == Male);
            AssertTwoListContainSameItems(actual.Select(c => c.Id), expected.Select(c => c.Id));
        }

        [Test]
        public void DetachedQueryTest()
        {
            var query = QueryOver.Of<Cat>().Where(c => c.Sex == Male);
            using var session = NHibernateHelper.OpenSession();
            var actual = query.GetExecutableQueryOver(session).List();
            var expected = _cats.Where(c => c.Sex == Male);
            AssertTwoListContainSameItems(actual.Select(c => c.Id), expected.Select(c => c.Id));
        }

        [Test]
        public void RestrictionProjectionOrderingTest()
        {
            using var session = NHibernateHelper.OpenSession();
            var actual = session.QueryOver<Cat>()
                .WhereRestrictionOn(c => c.Weight).IsBetween(2f).And(8f)
                .Select(c => c.Name)
                .OrderBy(c => c.Name).Asc
                .List<string>();
            var expected = _cats
                .Where(c => 2f <= c.Weight && c.Weight <= 8f)
                .Select(c => c.Name)
                .OrderBy(x => x);
            AssertTwoListContainSameItems(actual, expected);
        }

        [Test]
        public void AdditionRestrictionsTest()
        {
            using var session = NHibernateHelper.OpenSession();
            var actual = session.QueryOver<Cat>()
                .WhereRestrictionOn(c => c.Name).IsLike("%Cat%")
                .List();
            var expected = _cats.Where(c => c.Name.Contains("Cat"));
            AssertTwoListContainSameItems(actual.Select(c => c.Id), expected.Select(c => c.Id));

            actual = session.QueryOver<Cat>()
                .Where(Restrictions.On<Cat>(c => c.Name).IsLike("%Cat%"))
                .List();
            expected = _cats.Where(c => c.Name.Contains("Cat"));
            AssertTwoListContainSameItems(actual.Select(c => c.Id), expected.Select(c => c.Id));

            actual = session.QueryOver<Cat>()
                .Where(c => c.Name.IsLike("%Cat%"))
                .List();
            expected = _cats.Where(c => c.Name.Contains("Cat"));
            AssertTwoListContainSameItems(actual.Select(c => c.Id), expected.Select(c => c.Id));

            var catsToInclude = _cats.Take(3).Select(c => c.Name).ToList();
            actual = session.QueryOver<Cat>()
                .Where(c => c.Name.IsIn(catsToInclude) && c.Sex == Female)
                .List();
            expected = _cats.Where(c => catsToInclude.Contains(c.Name) && c.Sex == Female);
            AssertTwoListContainSameItems(actual.Select(c => c.Id), expected.Select(c => c.Id));
        }

        [Test]
        public void JoinQueryOverTest()
        {
            using var session = NHibernateHelper.OpenSession();
            // Default is inner join.
            var actual = session.QueryOver<CatStore>()
                .JoinQueryOver<Cat>(c => c.Cats)
                .Where(c => c.Weight > 6f)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
            var expected = _catStores.Where(cs => cs.Cats.Any(c => c.Weight > 6f));
            AssertTwoListContainSameItems(actual.Select(c => c.Id), expected.Select(c => c.Id));
        }

        [Test]
        public void JoinAliasTest()
        {
            using var session = NHibernateHelper.OpenSession();
            CatStore catStore = null;
            Cat cat = null;
            var catStoresToInclude = _catStores.Take(2).Select(cs => cs.Name).ToList();
            var actual = session.QueryOver(() => catStore)
                .JoinAlias(() => catStore.Cats, () => cat)
                .Where(() => catStore.Name.IsIn(catStoresToInclude))
                .And(() => cat.Sex == Female)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
            var expected = _catStores.Where(cs => catStoresToInclude.Contains(cs.Name) && cs.Cats.Any(c => c.Sex == Female));
            AssertTwoListContainSameItems(actual.Select(c => c.Id), expected.Select(c => c.Id));
        }

        [Test]
        public void ProjectionTest()
        {
            using var session = NHibernateHelper.OpenSession();
            var actual = session.QueryOver<Cat>()
                .Select(c => c.Name, c => c.Weight)
                .List<object[]>();

            var mappedActual = actual.Select(x => new
            {
                Name = (string)x[0],
                Weight = (float)x[1],
            });
            AssertTwoListContainSameItems(mappedActual.Select(c => c.Name), _cats.Select(c => c.Name));
        }

        [Test]
        public void ProjectionAndGroupTest()
        {
            using var session = NHibernateHelper.OpenSession();
            CatSumarry catSummary = null;
            var actual = session.QueryOver<Cat>()
                .SelectList(list => list
                    .SelectMax(c => c.Weight).WithAlias(() => catSummary.Max)
                    .SelectMin(c => c.Weight).WithAlias(() => catSummary.Min)
                    .SelectGroup(c => c.Sex).WithAlias(() => catSummary.Sex))
                .TransformUsing(Transformers.AliasToBean<CatSumarry>())
                .List<CatSumarry>();
            var expectedMale = _cats.Where(c => c.Sex == Male);
            var expectedFemale = _cats.Where(c => c.Sex == Female);
            actual.First(x => x.Sex == Male).Max.ShouldBe(expectedMale.Max(c => c.Weight));
            actual.First(x => x.Sex == Male).Min.ShouldBe(expectedMale.Min(c => c.Weight));
            actual.First(x => x.Sex == Female).Max.ShouldBe(expectedFemale.Max(c => c.Weight));
            actual.First(x => x.Sex == Female).Min.ShouldBe(expectedFemale.Min(c => c.Weight));
        }

        [Test]
        public void SubqueryTest()
        {
            using var session = NHibernateHelper.OpenSession();
            var avgWeight = QueryOver.Of<Cat>()
                .SelectList(x => x.SelectAvg(c => c.Weight));
            var actual = session.QueryOver<Cat>()
                .WithSubquery.WhereProperty(c => c.Weight).Gt(avgWeight)
                .List();
            var expected = _cats.Where(c1 => c1.Weight > _cats.Average(c2 => c2.Weight));
            AssertTwoListContainSameItems(actual.Select(c => c.Id), expected.Select(c => c.Id));

            actual = session.QueryOver<Cat>()
                .WithSubquery.Where(c => c.Weight > avgWeight.As<float>())
                .List();
            AssertTwoListContainSameItems(actual.Select(c => c.Id), expected.Select(c => c.Id));
        }

        class CatSumarry
        {
            public float Max { get; set; }
            public float Min { get; set; }
            public char Sex { get; set; }
        }

        private void PrepareDataForEachTest()
        {
            using var session = NHibernateHelper.OpenSession();
            using var transaction = session.BeginTransaction();
            _cats.Clear();
            _cats.AddRange(new[]
            {
                CreateCat("Cat 01"),
                CreateCat("Cat 02"),
                CreateCat("Cat 03"),
                CreateCat("Cat 04"),
                CreateCat("Cat 05"),
                CreateCat("Cat 06"),
                CreateCat("Cat 07"),
                CreateCat("Cat 08"),
                CreateCat("Cat 09"),
                CreateCat("Cat 10"),
            });
            _catStores.Clear();
            _catStores.AddRange(new[]
            {
                CreateCatStore("Cat store 1", _cats.Take(3).ToList()),
                CreateCatStore("Cat store 2", _cats.Skip(3).Take(3).ToList()),
                CreateCatStore("Cat store 3", _cats.Skip(6).ToList()),
            });
            foreach (var catStore in _catStores)
            {
                session.Save(catStore);
            }
            transaction.Commit();
        }

        private Cat CreateCat(string name)
        {
            return new Cat
            {
                Name = name,
                Sex = _random.Next(1, 100) >= 50 ? Female : Male,
                Weight = (float)_random.NextDouble() * 10,
            };
        }

        private CatStore CreateCatStore(string name, List<Cat> cats)
        {
            return new CatStore
            {
                Name = name,
                Cats = cats,
            };
        }

        private void AssertTwoListContainSameItems<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            list1.ToHashSet().SetEquals(list2.ToHashSet()).ShouldBeTrue();
        }
    }
}