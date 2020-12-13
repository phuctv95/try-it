using Microsoft.Extensions.Logging;
using System;

namespace TryAspNetCore.Services
{
    public class MyDependency : IMyDependency
    {
        private readonly ILogger<MyDependency> _logger;

        public MyDependency(ILogger<MyDependency> logger)
        {
            _logger = logger;
        }

        public void WriteMessage(string message)
        {
            _logger.LogInformation(message);
        }
    }
}
