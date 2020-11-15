using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace TryConsole.Tests
{
    [TestClass()]
    public class HelperTests
    {
        [TestMethod()]
        public void Expression100Test()
        {
            var computer = new DataTable();
            foreach (var expression in Helper.Expression100())
            {
                var actual = computer.Compute(expression, string.Empty);
                Assert.AreEqual(100, actual);
            }
        }
    }
}