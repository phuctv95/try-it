using System;
using TryNHibernate.Domain;

namespace TryNHibernate
{
    public class Program
    {
        static void Main(string[] args)
        {
            IProductRepository repo = new ProductRepository();
            var product = new Product
            {
                Name = "Google Pixel 2XL",
                Category = "Google",
                Discontinued = false
            };
            repo.Add(product);
            Console.WriteLine(product.Id);
        }
    }
}
