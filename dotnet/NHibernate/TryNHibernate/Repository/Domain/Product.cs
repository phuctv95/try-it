using System;

namespace Repository.Domain
{
    public class Product
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; } = string.Empty;
        public virtual string Category { get; set; } = string.Empty;
        public virtual bool Discontinued { get; set; }
    }
}
