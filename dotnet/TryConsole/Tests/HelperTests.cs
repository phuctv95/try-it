using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

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
}