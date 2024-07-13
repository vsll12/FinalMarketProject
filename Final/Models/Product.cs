using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Models
{
    internal class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Product() { }

        public Product(string? name, string? description, string? category, double price, int quantity)
        {
            Name = name;
            Description = description;
            Category = category;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{Name}  {Price}  {Description}";
        }

        public void ShowInfo()
        {
            Console.Write($"{Name}  {Description}  {Category}  {Price}  {Quantity}");
        }
    }
}
