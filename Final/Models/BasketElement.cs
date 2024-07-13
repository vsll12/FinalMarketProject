namespace Final.Models
{
    internal class BasketElement
    {
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; } 

        public BasketElement() { }
        public BasketElement(string name , int quantity, double price)
        {
            this.Quantity = quantity;
            this.ProductName = name;
            this.Price = price;
        }
    }
}
