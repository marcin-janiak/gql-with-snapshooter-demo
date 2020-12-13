using System;
using System.Collections.Generic;

namespace API
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual Guid? SupervisorId { get; set; }
        public virtual Person? Supervisor { get; set; }
    }
}