using System;
using Hangfire;

namespace HangfireServer
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(
                "Server=.; Database=HangfireTest; Integrated Security=True");

            using (new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press ENTER to exit...");
                Console.ReadLine();
            }
        }
    }
}
