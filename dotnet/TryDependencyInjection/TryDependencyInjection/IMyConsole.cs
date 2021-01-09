using System.Collections.Generic;

namespace TryDependencyInjection
{
    public interface IMyConsole
    {
        void Write(object value);
        void WriteLine(object value);
        string ReadLine();
    }
}
