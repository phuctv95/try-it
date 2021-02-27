﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TryConsole.Tests
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
                Assert.AreEqual(100, actual);
            }
        }

        [TestMethod()]
        public void Overwrite()
        {
            string content = Guid.NewGuid().ToString();
            const string FilePath = "test.txt";
            Helper.Overwrite(FilePath, content);

            Assert.IsTrue(File.Exists(FilePath));
            var lines = File.ReadAllLines(FilePath);
            Assert.AreEqual(1, lines.Length);
            Assert.AreEqual(content, lines[0]);
            File.Delete(FilePath);
        }

        [TestMethod()]
        public void TestReflection()
        {
            Assert.AreEqual(123.GetType(), Type.GetType("System.Int32"));
            Assert.AreEqual(123.GetType(), typeof(int));

            var type = Type.GetType("TryConsole.Tests.TestClass");

            Assert.IsNotNull(type);
            Assert.AreEqual(typeof(int), type!.GetProperty("X")?.PropertyType);
            Assert.AreEqual(typeof(string), type!.GetProperty("Y")?.PropertyType);
            Assert.AreEqual(null, type!.GetProperty("Z")?.PropertyType);
            Assert.AreEqual(typeof(string), type!.GetProperty("Z", BindingFlags.NonPublic | BindingFlags.Instance)?.PropertyType);

            Assert.AreEqual(typeof(string), type!.GetMethod("GetValue")?.ReturnType);
            Assert.IsNotNull(type!.GetConstructor(new Type[] { typeof(int) }));

            var assembly = Assembly.Load("System");
            Assert.AreEqual("System.dll", assembly.ManifestModule.Name);
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
            Assert.AreEqual(expected, Helper.Fibonacci(n));
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

            Assert.AreEqual(Color.Red, GetColor(MyColor.Red));
            Assert.AreEqual(Color.Green, GetColor(MyColor.Green));
            Assert.AreEqual(Color.Blue, GetColor(MyColor.Blue));
            Assert.ThrowsException<ArgumentException>(() => GetColor((MyColor)100));

            // Property pattern.
            Func<TestClass, int> ParseY = instance => instance switch
            {
                { Y: "1" } => 1,
                { Y: "2" } => 2,
                { Y: "3" } => 3,
                _ => throw new NotSupportedException(),
            };

            Assert.AreEqual(1, ParseY(new TestClass { Y = "1" }));
            Assert.AreEqual(2, ParseY(new TestClass { Y = "2" }));
            Assert.AreEqual(3, ParseY(new TestClass { Y = "3" }));

            // Tuple pattern.
            Func<MyColor, MyColor, Color> MixColor = (c1, c2) => (c1, c2) switch
            {
                (MyColor.Red, MyColor.Green) => Color.Yellow,
                (MyColor.Red, MyColor.Blue) => Color.Magenta,
                (MyColor.Green, MyColor.Blue) => Color.Cyan,
                (_, _) => throw new NotSupportedException(),
            };
            Assert.AreEqual(Color.Yellow, MixColor(MyColor.Red, MyColor.Green));
            Assert.AreEqual(Color.Magenta, MixColor(MyColor.Red, MyColor.Blue));
            Assert.AreEqual(Color.Cyan, MixColor(MyColor.Green, MyColor.Blue));

            // Positional pattern.
            var myTestClass = new TestClass { X = 1, Y = "2" };
            if (myTestClass is TestClass(int x, string y))
            {
                Assert.AreEqual(myTestClass.X, x);
                Assert.AreEqual(myTestClass.Y, y);
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
            Assert.AreEqual(10, counter);
        }


        [TestMethod]
        public void AsynchronousDispose()
        {

        }

        [TestMethod]
        public void IndicesAndRange()
        {
            var arr = new string[] { "a", "b", "c", "d", "e" };

            Assert.AreEqual("a", arr[0]);
            Assert.AreEqual("b", arr[1]);
            Assert.AreEqual("e", arr[^1]);
            Assert.AreEqual("d", arr[^2]);

            Assert.ThrowsException<IndexOutOfRangeException>(() => arr[^0]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => arr[^(arr.Length + 1)]);

            Assert.IsTrue(arr[0..2].SequenceEqual(new string[] { "a", "b" }));
            Assert.IsTrue(arr[..2].SequenceEqual(new string[] { "a", "b" }));
            Assert.IsTrue(arr[3..].SequenceEqual(new string[] { "d", "e" }));
            Assert.IsTrue(arr[^2..].SequenceEqual(new string[] { "d", "e" }));

            var range = ^2..;
            Assert.IsTrue(arr[range].SequenceEqual(new string[] { "d", "e" }));
        }

        [TestMethod]
        public void NullCoalescingAssignment()
        {
            int? x = 1;
            x ??= 2;
            Assert.AreEqual(1, x);

            x = null;
            x ??= 2;
            Assert.AreEqual(2, x);
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

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(cities.First(), result.First());
            Assert.IsTrue(cities.Select(x => x.IsBig).SequenceEqual(new bool[] { true, false }));
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

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(cities.Skip(1).First(), result.First());
            Assert.IsTrue(cities.Select(x => x.IsBig).SequenceEqual(new bool[] { true, true }));
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