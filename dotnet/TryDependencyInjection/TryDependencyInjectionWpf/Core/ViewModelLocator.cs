namespace TryDependencyInjectionWpf.Core
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => IocKernel.Get<MainViewModel>();
    }
}
