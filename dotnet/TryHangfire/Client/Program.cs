using System;
using System.Threading;
using Hangfire;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(
                "Server=.; Database=HangfireTest; Integrated Security=True");
            
            BackgroundJob.Enqueue(
                () => Console.WriteLine("Fire-and-forget."));

            BackgroundJob.Schedule(
                () => Console.WriteLine("Delayed 5 seconds."),
                TimeSpan.FromSeconds(5));

            var id = Guid.NewGuid().ToString();
            RecurringJob.AddOrUpdate(
                id,
                () => Console.WriteLine($"{DateTime.Now} A message every minute."),
                Cron.Minutely);
            Thread.Sleep(TimeSpan.FromMinutes(3));
            RecurringJob.RemoveIfExists(id);
        }
    }
}
