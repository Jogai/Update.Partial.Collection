namespace Example.Models
{
    public class ChildSchema
    {
        public int Id { get; set; }

        public int ChildId { get; set; }

        public Child Child { get; set; }

        public int SchemaId { get; set; }

        public Schema Schema { get; set; }
    }
}