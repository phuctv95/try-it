using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Drawing;
using System.Reflection;
using TryConsole;

namespace Tests
{
    [TestClass]
    public class HelperTests
    {
        [TestMethod]
        public void Expression100Test()
        {
            var computer = new DataTable();
            foreach (var expression in Helper.Expression100())
            {
                var actual = computer.Compute(expression, string.Empty);
                actual.Should().Be(100);
            }
        }

        [TestMethod()]
        public void Overwrite()
        {
            string content = Guid.NewGuid().ToString();
            const string FilePath = "test.txt";
            Helper.Overwrite(FilePath, content);

            File.Exists(FilePath).Should().BeTrue();
            var lines = File.ReadAllLines(FilePath);
            lines.Length.Should().Be(1);
            lines[0].Should().Be(content);
            File.Delete(FilePath);
        }

        [TestMethod()]
        public void TestReflection()
        {
            123.GetType()
                .Should().Be(Type.GetType("System.Int32"));
            123.GetType()
                .Should().Be(typeof(int));

            var type = Type.GetType("Tests.TestClass");

            type.Should().NotBeNull();
            type!.GetProperty("X")?.PropertyType
                .Should().Be(typeof(int));
            type!.GetProperty("Y")?.PropertyType
                .Should().Be(typeof(string));
            type!.GetProperty("Z")?.PropertyType
                .Should().BeNull();
            type!.GetProperty("Z", BindingFlags.NonPublic | BindingFlags.Instance)?.PropertyType
                .Should().Be(typeof(string));

            type!.GetMethod("GetValue")?.ReturnType
                .Should().Be(typeof(string));
            type!.GetConstructor(new Type[] { typeof(int) })
                .Should().NotBeNull();

            var assembly = Assembly.Load("System");
            "System.dll".Should().Be(assembly.ManifestModule.Name);
        }

        [TestMethod]
        #region Test cases
        [DataRow(1, 1)]
        [DataRow(2, 1)]
        [DataRow(3, 2)]
        [DataRow(4, 3)]
        [DataRow(5, 5)]
        [DataRow(6, 8)]
        [DataRow(7, 13)]
        [DataRow(8, 21)]
        [DataRow(9, 34)]
        [DataRow(10, 55)]
        #endregion
        public void Fibonacci(int n, int expected)
        {
            expected.Should().Be(Helper.Fibonacci(n));
        }

        [TestMethod]
        public void PatternMatching()
        {
            // Switch expression.
            Func<MyColor, Color> GetColor = color => color switch
            {
                MyColor.Red => Color.Red,
                MyColor.Green => Color.Green,
                MyColor.Blue => Color.Blue,
                _ => throw new ArgumentException(message: "Invalid param.", paramName: nameof(color)),
            };

            GetColor(MyColor.Red).Should().Be(Color.Red);
            GetColor(MyColor.Green).Should().Be(Color.Green);
            GetColor(MyColor.Blue).Should().Be(Color.Blue);
            FluentActions.Invoking(() => GetColor((MyColor)100)).Should().Throw<ArgumentException>();

            // Property pattern.
            Func<TestClass, int> ParseY = instance => instance switch
            {
                { Y: "1" } => 1,
                { Y: "2" } => 2,
                { Y: "3" } => 3,
                _ => throw new NotSupportedException(),
            };

            ParseY(new TestClass { Y = "1" }).Should().Be(1);
            ParseY(new TestClass { Y = "2" }).Should().Be(2);
            ParseY(new TestClass { Y = "3" }).Should().Be(3);

            // Tuple pattern.
            Func<MyColor, MyColor, Color> MixColor = (c1, c2) => (c1, c2) switch
            {
                (MyColor.Red, MyColor.Green) => Color.Yellow,
                (MyColor.Red, MyColor.Blue) => Color.Magenta,
                (MyColor.Green, MyColor.Blue) => Color.Cyan,
                (_, _) => throw new NotSupportedException(),
            };
            MixColor(MyColor.Red, MyColor.Green).Should().Be(Color.Yellow);
            MixColor(MyColor.Red, MyColor.Blue).Should().Be(Color.Magenta);
            MixColor(MyColor.Green, MyColor.Blue).Should().Be(Color.Cyan);

            // Positional pattern.
            var myTestClass = new TestClass { X = 1, Y = "2" };
            if (myTestClass is TestClass(int x, string y))
            {
                x.Should().Be(myTestClass.X);
                y.Should().Be(myTestClass.Y);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task AsynchronousStream()
        {
            var counter = 0;
            // Continue on next statement on caller after this await.
            await foreach (var item in Helper.GenerateSequence(10))
            {
                counter++;
                Console.WriteLine($"Report received item: {item}.");
            }
            counter.Should().Be(10);
        }


        [TestMethod]
        public void AsynchronousDispose()
        {

        }

        [TestMethod]
        public void IndicesAndRange()
        {
            var arr = new string[] { "a", "b", "c", "d", "e" };

            arr[0].Should().Be("a");
            arr[1].Should().Be("b");
            arr[^1].Should().Be("e");
            arr[^2].Should().Be("d");

            FluentActions.Invoking(() => arr[^0]).Should().Throw<IndexOutOfRangeException>();
            FluentActions.Invoking(() => arr[^(arr.Length + 1)]).Should().Throw<IndexOutOfRangeException>();

            Assert.IsTrue(arr[0..2].SequenceEqual(new string[] { "a", "b" }));
            Assert.IsTrue(arr[..2].SequenceEqual(new string[] { "a", "b" }));
            Assert.IsTrue(arr[3..].SequenceEqual(new string[] { "d", "e" }));
            Assert.IsTrue(arr[^2..].SequenceEqual(new string[] { "d", "e" }));
            arr[0..2].Should().BeEquivalentTo(new string[] { "a", "b" }, opt => opt.WithStrictOrdering());
            arr[..2].Should().BeEquivalentTo(new string[] { "a", "b" }, opt => opt.WithStrictOrdering());
            arr[3..].Should().BeEquivalentTo(new string[] { "d", "e" }, opt => opt.WithStrictOrdering());
            arr[^2..].Should().BeEquivalentTo(new string[] { "d", "e" }, opt => opt.WithStrictOrdering());

            var range = ^2..;
            arr[range].Should().BeEquivalentTo(new string[] { "d", "e" }, opt => opt.WithStrictOrdering());
        }

        [TestMethod]
        public void NullCoalescingAssignment()
        {
            int? x = 1;
            x ??= 2;
            x.Should().Be(1);

            x = null;
            x ??= 2;
            x.Should().Be(2);
        }

        [TestMethod]
        public void TryLinq1()
        {
            var cities = new []
            {
                new City { Name = "Abc", IsBig = true, },
                new City { Name = "Def", IsBig = false, },
            };
            
            var result = cities
                .Where(c =>
                    {
                        var temp = c.IsBig;
                        c.IsBig = true;
                        return temp;
                    })
                .Take(1)
                .ToList();

            result.Count.Should().Be(1);
            result.First().Should().Be(cities.First());
            cities.Select(x => x.IsBig)
                .Should().BeEquivalentTo(new bool[] { true, false }, opt => opt.WithStrictOrdering());
        }

        [TestMethod]
        public void TryLinq2()
        {
            var cities = new[]
            {
                new City { Name = "Abc", IsBig = false, },
                new City { Name = "Def", IsBig = true, },
            };

            var result = cities
                .Where(c =>
                {
                    var temp = c.IsBig;
                    c.IsBig = true;
                    return temp;
                })
                .Take(1)
                .ToList();

            result.Count.Should().Be(1);
            result.First().Should().Be(cities.Skip(1).First());
            cities.Select(x => x.IsBig)
                .Should().BeEquivalentTo(new bool[] { true, true }, opt => opt.WithStrictOrdering());
        }
    }

    class TestClass
    {
        public int X { get; set; }
        public string Y { get; set; } = string.Empty;
        private string Z { get; set; } = string.Empty;
        public TestClass() { }
        public TestClass(int x)
        {
            X = x;
        }

        public string GetValue() => $"{X} {Y} {Z}";

        public void Deconstruct(out int x, out string y) => (x, y) = (X, Y);
    }

    class City
    {
        public string Name { get; set; }
        public bool IsBig { get; set; }
    }

    enum MyColor { Red, Green, Blue }
}