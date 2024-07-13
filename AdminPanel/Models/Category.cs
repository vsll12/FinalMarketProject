namespace AdminSide.Models
{
    internal class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Product>? Products { get; set; } = new List<Product>();

        public Category() { }

        public Category(string name, int id)
        {
            Name = name;
            Id = id;
        }

    }
}
