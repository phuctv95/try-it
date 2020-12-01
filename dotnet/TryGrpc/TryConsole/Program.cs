using Grpc.Net.Client;
using System;
using TryGrpc;

namespace TryConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var response = client.SayHello(new HelloRequest { Name = "Leo" });
            Console.WriteLine(response.Message);
        }
    }
}
