using AutoMapper;
using DataAccess;
using Ninject;
using Ninject.Activation;
using System;
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
            kernel.Bind<IMapper>().ToMethod(ConfigureMapper).InSingletonScope();
            kernel.Get<BookConsole>().Run();
        }

        private static IMapper ConfigureMapper(IContext context)
        {
            const string IsAvailableText = "Is available";
            const string IsNotAvailableText = "Not available";
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Book, BookRepresentation>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(resolver => resolver.Id.ToString()))
                    .ForMember(dest => dest.Available, opt => opt.MapFrom(resolver => resolver.Available ? IsAvailableText : IsNotAvailableText))
                    .ForMember(dest => dest.ExpensiveLevel, opt => opt.MapFrom(resolver => resolver.Price < 100 ? "Cheap" : resolver.Price > 1000 ? "Expensive" : "Normal"));
                cfg.CreateMap<BookRepresentation, Book>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(resolver => Guid.Parse(resolver.Id)))
                    .ForMember(dest => dest.Available, opt => opt.MapFrom(resolver => resolver.Available == IsAvailableText));
            });
            return config.CreateMapper();
        }
    }
}
