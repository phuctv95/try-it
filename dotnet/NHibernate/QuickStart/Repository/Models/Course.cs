using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public class Course
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; } = string.Empty;
        public virtual IList<Student> Students { get; set; } = new List<Student>();
    }
}
