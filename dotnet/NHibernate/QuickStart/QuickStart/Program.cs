using System;
using RepositoryMapByCode.Repositories;

namespace QuickStart
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            NHibernateHelper.ResetSchema();
        }
    }
}
