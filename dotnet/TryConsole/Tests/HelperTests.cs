using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.IO;
using System.Reflection;

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
        public void Fibonacci(int n, int expected)
        {
            Assert.AreEqual(expected, Helper.Fibonacci(n));
        }
    }

    class TestClass
    {
        public int X { get; set; }
        public string Y { get; set; } = string.Empty;
        private string Z { get; set; } = string.Empty;
        public TestClass(int x)
        {
            X = x;
        }

        public string GetValue() => $"{X} {Y} {Z}";
    }
}