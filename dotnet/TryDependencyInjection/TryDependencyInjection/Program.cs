using DataAccess;
using Ninject;
using System.Text;

namespace TryDependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var kernel = new StandardKernel();
            kernel.Bind<IBookRepository>().To<BookCsvRepository>();
            kernel.Bind<IMyConsole>().To<MyConsole>();
            kernel.Get<BookConsole>().Run();
        }
    }
}
