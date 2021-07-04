using System;

namespace RepositoryMapByCode.Models
{
    public class Cat
    {
        public virtual string Id { get; set; } = string.Empty;
        public virtual string Name { get; set; } = string.Empty;
        public virtual char Sex { get; set; }
        public virtual float Weight { get; set; }
        public virtual CatStore? CatStore { get; set; }
        public virtual DateTime Version { get; set; }
    }
}
