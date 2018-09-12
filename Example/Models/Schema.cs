using System;
using System.Collections.Generic;

namespace Example.Models
{
    public class Schema
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public Parent Parent { get; set; }

        public DateTime Visit { get; set; }

        public ICollection<ChildSchema> ChildSchemas { get; set; }
    }
}