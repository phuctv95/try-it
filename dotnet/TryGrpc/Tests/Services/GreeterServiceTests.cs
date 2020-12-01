using Grpc.Net.Client;
using NUnit.Framework;
using TryGrpc;

namespace Tests.Services
{
    class GreeterServiceTests
    {
        private const string Address = "https://localhost:5001";
        private Greeter.GreeterClient _client;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var channel = GrpcChannel.ForAddress(Address);
            _client = new Greeter.GreeterClient(channel);
        }

        [Test]
        public void SayHelloTest()
        {
            const string Name = "World";

            var response = _client.SayHello(new HelloRequest { Name = Name });

            Assert.AreEqual($"Hello {Name}", response.Message);
        }
    }
}
