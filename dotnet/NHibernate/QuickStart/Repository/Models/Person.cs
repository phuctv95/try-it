using System;
using System.Collections;

namespace Repository.Models
{
    public class Person
    {
        public virtual Guid Id { get; set; }
        public virtual IDictionary? Attributes { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
