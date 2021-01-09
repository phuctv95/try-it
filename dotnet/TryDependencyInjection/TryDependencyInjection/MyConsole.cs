using System;

namespace TryDependencyInjection
{
    public class MyConsole : IMyConsole
    {
        public void Write(object value) => Console.Write(value);
        public void WriteLine(object value) => Console.WriteLine(value);
        public string ReadLine() => Console.ReadLine();
    }
}
