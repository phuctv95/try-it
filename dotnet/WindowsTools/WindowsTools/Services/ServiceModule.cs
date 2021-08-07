using Ninject.Modules;

namespace WindowsTools.Services
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ThemeTool>().ToSelf().InSingletonScope();
            Bind<MessageBoxExtra>().ToSelf().InSingletonScope();
        }
    }
}
