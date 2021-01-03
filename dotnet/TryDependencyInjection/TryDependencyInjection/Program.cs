using DataAccess;
using Ninject;

namespace TryDependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Bind<IBookRepository>().To<BookCsvRepository>();
            kernel.Get<BookConsole>().Run();
        }
    }
}
