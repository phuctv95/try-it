using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public class Student
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; } = string.Empty;
        public virtual IList<Course> Courses { get; set; } = new List<Course>();
    }
}
