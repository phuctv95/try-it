using System;

namespace DataAccess
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool Available { get; set; }
    }
}
