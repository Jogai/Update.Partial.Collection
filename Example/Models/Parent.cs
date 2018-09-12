using System;
using System.Collections.Generic;

namespace Example.Models
{
    public class Parent
    {
        public int Id { get; set; }

        public DateTime KeyDate { get; set; }

        public string Name { get; set; }

        public ICollection<Schema> Schemas { get; set; }
    }
}