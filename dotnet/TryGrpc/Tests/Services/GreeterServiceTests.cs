using Grpc.Net.Client;
using NUnit.Framework;
using TryGrpc;

namespace Tests.Services
{
    class GreeterServiceTests
    {
        private const string Address = "https://localhost:5001";

        [Test]
        public void Test1()
        {
            const string Name = "World";
            var channel = GrpcChannel.ForAddress(Address);
            var client = new Greeter.GreeterClient(channel);

            var response = client.SayHello(new HelloRequest { Name = Name });

            Assert.AreEqual($"Hello {Name}", response.Message);
        }
    }
}
