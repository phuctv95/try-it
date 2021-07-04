using System;

namespace Repository.Models
{
    public class Employee
    {
        public virtual Guid Id { get; set; }
        public virtual string Role { get; set; } = string.Empty;
        public virtual Person? Person { get; set; }
    }
}
