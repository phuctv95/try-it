using System;
using System.Collections.Generic;

namespace RepositoryMapByCode.Models
{
    public class CatStore
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; } = string.Empty;
        public virtual IList<Cat> Cats { get; set; } = new List<Cat>();
        public virtual ISet<string> PhoneNumbers { get; set; } = new SortedSet<string>();
    }
}