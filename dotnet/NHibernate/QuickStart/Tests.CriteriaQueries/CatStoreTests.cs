using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using NUnit.Framework;
using RepositoryMapByCode.Models;
using RepositoryMapByCode.Repositories;
using Shouldly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tests.CriteriaQueries
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
            var criteria = session.CreateCriteria<Cat>();

            var cats = criteria.SetMaxResults(2).List<Cat>();

            cats.Count.ShouldBe(2);
            _cats.FirstOrDefault(c => c.Id == cats[0].Id).ShouldNotBeNull();
            _cats.FirstOrDefault(c => c.Id == cats[1].Id).ShouldNotBeNull();
        }

        [Test]
        public void NarrowingResultTest()
        {
            using var session = NHibernateHelper.OpenSession();

            var actual = session
                .CreateCriteria<Cat>()
                .Add(Restrictions.Like("Name", "Cat%"))
                .Add(Restrictions.Between("Weight", 1f, 4f))
                .List<Cat>();
            var expected = _cats
                .Where(c => c.Name.StartsWith("Cat") && 1f <= c.Weight && c.Weight <= 4f)
                .ToList();
            AssertTwoListContainSameItems(actual.Select(c => c.Id), expected.Select(c => c.Id));

            actual = session
                .CreateCriteria<Cat>()
                .Add(Restrictions.Like("Name", "Cat%"))
                .Add(Restrictions.Or(
                    Restrictions.Eq("Sex", Female),
                    Restrictions.Gt("Weight", 2f)))
                .List<Cat>();
            expected = _cats
                .Where(c => c.Name.StartsWith("Cat") && (c.Sex == Female || c.Weight > 2f))
                .ToList();
            AssertTwoListContainSameItems(actual.Select(c => c.Id), expected.Select(c => c.Id));

            actual = session
                .CreateCriteria<Cat>()
                .Add(Restrictions.In("Name", new [] { "Cat 1", "Cat 2", "Cat 3" }))
                .Add(Restrictions.Disjunction() // OR
                    .Add(Restrictions.Eq("Sex", Female))
                    .Add(Restrictions.Gt("Weight", 4f))
                    .Add(Restrictions.Lt("Weight", 2f)))
                .List<Cat>();
            expected = _cats
                .Where(c => (new[] { "Cat 1", "Cat 2", "Cat 3" }).Contains(c.Name) && (c.Sex == Female || c.Weight > 2f))
                .ToList();
            AssertTwoListContainSameItems(actual.Select(c => c.Id), expected.Select(c => c.Id));
        }

        [Test]
        public void SqlTest()
        {
            using var session = NHibernateHelper.OpenSession();

            var actual = session
                .CreateCriteria<Cat>()
                // {alias} is the root alias (Cat table)
                .Add(Expression.Sql("lower({alias}.Name) like lower(?)", "cat%", NHibernateUtil.String))
                .List<Cat>();
            var expected = _cats
                .Where(c => c.Name.ToLower().StartsWith("cat"))
                .ToList();
            AssertTwoListContainSameItems(actual.Select(c => c.Id), expected.Select(c => c.Id));
        }

        [Test]
        public void OrderingTest()
        {
            using var session = NHibernateHelper.OpenSession();
            var actual = session.CreateCriteria<Cat>()
                .AddOrder(Order.Asc("Sex"))
                .AddOrder(Order.Desc("Name"))
                .List<Cat>();
            var expected = _cats
                .OrderBy(c => c.Sex)
                .ThenByDescending(c => c.Name)
                .ToList();
            actual.Select(c => c.Id)
                .SequenceEqual(expected.Select(c => c.Id))
                .ShouldBeTrue();
        }

        [Test]
        public void AssociationTest()
        {
            using var session = NHibernateHelper.OpenSession();
            var actual = session.CreateCriteria<CatStore>()
                .Add(Restrictions.Like("Name", "Cat store%"))
                .CreateCriteria("Cats")
                    .Add(Restrictions.Like("Sex", Male))
                .List<CatStore>();
            // The query above generated a JOIN query so the result was rows of duplicated cat stores,
            // but different in cats -> the rows database provided was distinguish but was duplicated
            // in program memory -> call Distinct() here.
            actual = actual.Distinct().ToList();
            var expected = _catStores.Where(cs => cs.Name.StartsWith("Cat store") && cs.Cats.Any(c => c.Sex == Male));
            AssertTwoListContainSameItems(actual.Select(cs => cs.Id), expected.Select(cs => cs.Id));

            // This criteria generates the same SQL query as the previous.
            actual = session.CreateCriteria<CatStore>()
                .Add(Restrictions.Like("Name", "Cat store%"))
                .CreateAlias("Cats", "cat")
                .Add(Restrictions.Like("cat.Sex", Male))
                .List<CatStore>();
            actual = actual.Distinct().ToList();
            AssertTwoListContainSameItems(actual.Select(cs => cs.Id), expected.Select(cs => cs.Id));

            // This criteria generates the same SQL query as the previous.
            actual = session.CreateCriteria<CatStore>()
                .Add(Restrictions.Like("Name", "Cat store%"))
                .CreateAlias("Cats", "cat")
                .Add(Restrictions.Like("cat.Sex", Male))
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .List<CatStore>();
            AssertTwoListContainSameItems(actual.Select(cs => cs.Id), expected.Select(cs => cs.Id));
        }

        [Test]
        public void AssociationWithAliasToEntityMapTest()
        {
            using var session = NHibernateHelper.OpenSession();
            var actual = session.CreateCriteria<CatStore>()
                .Add(Restrictions.Like("Name", "Cat store%"))
                .CreateAlias("Cats", "cat")
                .Add(Restrictions.Like("cat.Sex", Male))
                .SetResultTransformer(Transformers.AliasToEntityMap)
                .List();
            var expected = _catStores
                .SelectMany(cs => cs.Cats, (cs, c) => new { CatStore = cs, Cat = c })
                .Where(x => x.CatStore.Name.StartsWith("Cat store") && x.Cat.Sex == Male)
                .ToList();
            actual.Count.ShouldBe(expected.Count);
            foreach (IDictionary map in actual)
            {
                expected.FirstOrDefault(x => x.CatStore.Id == (map["this"] as CatStore).Id).ShouldNotBeNull();
                expected.FirstOrDefault(x => x.Cat.Id == (map["cat"] as Cat).Id).ShouldNotBeNull();
            }
        }

        [Test]
        public void ForceFetchingTest()
        {
            using var session = NHibernateHelper.OpenSession();
            // This produces a JOIN query to fetch cats.
            var actual = session.CreateCriteria<CatStore>()
                .Add(Restrictions.Eq("Name", _catStores[0].Name))
                .Fetch(SelectMode.Fetch, "Cats")
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .List<CatStore>();

            var expected = _catStores.First(cs => cs.Name == _catStores[0].Name);
            actual.Count.ShouldBe(1);
            actual.First().Id.ShouldBe(expected.Id);
            AssertTwoListContainSameItems(
                actual.First().Cats.Select(c => c.Id),
                expected.Cats.Select(c => c.Id));
        }

        [Test]
        public void QueryByExampleTest()
        {
            using var session = NHibernateHelper.OpenSession();
            var cat = new Cat { Sex = _cats[0].Sex };

            var actual = session.CreateCriteria<Cat>()
                .Add(Example.Create(cat).ExcludeZeroes().ExcludeProperty("Name"))
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .List<Cat>();

            var expected = _cats.Where(c => c.Sex == _cats[0].Sex);
            AssertTwoListContainSameItems(actual.Select(c => c.Id), expected.Select(c => c.Id));
        }

        [Test]
        public void ProjectionTest()
        {
            // We can think of projection is setting up a SELECT.
            using var session = NHibernateHelper.OpenSession();
            var actual = session.CreateCriteria<Cat>()
                .SetProjection(Projections.RowCount())
                .Add(Restrictions.Eq("Sex", Male))
                .List();
            var expected = _cats.Count(c => c.Sex == Male);
            actual[0].ShouldBe(expected);
        }

        [Test]
        public void ProjectionAndGroupTest()
        {
            using var session = NHibernateHelper.OpenSession();
            var actual = session.CreateCriteria<Cat>()
                .SetProjection(Projections.ProjectionList()
                    .Add(Projections.RowCount())
                    .Add(Projections.Avg("Weight"))
                    .Add(Projections.Max("Weight"))
                    .Add(Projections.GroupProperty("Sex")))
                .List();

            var expected = _cats
                .GroupBy(c => c.Sex)
                .Select(x => new
                {
                    Count = x.Count(),
                    Avg = x.Average(c => c.Weight),
                    Max = x.Max(c => c.Weight),
                    Sex = x.Key,
                })
                .ToList();
            actual.Count.ShouldBe(expected.Count);
            foreach (var item in actual)
            {
                var row = (item as IList);
                var expectedItem = expected.First(c => c.Sex == (char)row[3]);
                row[0].ShouldBe(expectedItem.Count);
                Math.Round((double)row[1], 3).ShouldBe(Math.Round(expectedItem.Avg, 3));
                Math.Round((float)row[2], 3).ShouldBe(Math.Round(expectedItem.Max, 3));
            }
        }

        [Test]
        public void ProjectionAliasTest()
        {
            // Projection can be aliased so it can be referenced later in Order query or in a restriction.
            // Syntax detail: https://nhibernate.info/previous-doc/v4.1/ref/querycriteria.html#querycriteria-projection
        }

        [Test]
        public void DetachedQueryTest()
        {
            // We can create a query outside of a session and then use it later in a session.
            var query = DetachedCriteria
                .For<Cat>()
                .Add(Restrictions.Eq("Sex", Female));

            using var session = NHibernateHelper.OpenSession();
            var actual = query.GetExecutableCriteria(session).List<Cat>();

            var expected = _cats.Where(c => c.Sex == Female);
            AssertTwoListContainSameItems(actual.Select(c => c.Id), expected.Select(c => c.Id));
        }

        [Test]
        public void DetachQueryAsSubqueryTest()
        {
            using var session = NHibernateHelper.OpenSession();

            var avgWeight = DetachedCriteria
                .For<Cat>()
                .SetProjection(Projections.Avg("Weight"));
            var actual = session
                .CreateCriteria<Cat>()
                .Add(Subqueries.PropertyGt("Weight", avgWeight))
                .List<Cat>();

            var avgWeightExpected = _cats.Average(c => c.Weight);
            var expected = _cats.Where(c => c.Weight > avgWeightExpected);
            AssertTwoListContainSameItems(actual.Select(c => c.Id), expected.Select(c => c.Id));

            var weights = DetachedCriteria
                .For<Cat>()
                .SetProjection(Projections.Property("Weight"));
            actual = session
                .CreateCriteria<Cat>()
                .Add(Subqueries.PropertyGeAll("Weight", weights))
                .List<Cat>();
            var expectedCat = _cats.OrderByDescending(c => c.Weight).First();
            actual.Count.ShouldBe(1);
            actual.First().Id.ShouldBe(expectedCat.Id);
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
            _catStores.AddRange(new []
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