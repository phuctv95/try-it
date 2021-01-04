using AutoMapper;
using DataAccess;
using Ninject.Modules;

namespace TryDependencyInjectionWpf.Core
{
    public class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IBookRepository>().To<BookCsvRepository>().InSingletonScope();
            Bind<IMapper>().ToMethod(context =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Book, BookModel>();
                    cfg.CreateMap<BookModel, Book>();
                });
                return config.CreateMapper();
            }).InSingletonScope();
        }
    }
}
