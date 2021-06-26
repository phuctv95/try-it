using System.Collections.Generic;

namespace TryEntityFramework.Models
{
    public class Person : Entity
    {
        public string Name { get; set; }

        public List<Motorbike> Motorbikes { get; set; }
    }
}
