using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TupleTests
    {
        [TestMethod]
        public void Deconstruct()
        {
            var x = ("hello", 123);
            var (y, z) = x;
            y.Should().Be(x.Item1);
            z.Should().Be(x.Item2);
        }
    }
}
