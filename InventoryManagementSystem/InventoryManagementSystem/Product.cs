using System;

namespace InventoryManagementSystem
{
    internal class Product
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        // Constructor for creating a new product
        public Product(
            int id,
            string name,
            int quantity,
            decimal price)
        {
            ID = id;
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        // Needed when loading from JSON
        public Product()
        {
            Name = "";
        }

        public decimal GetTotalPrice()
        {
            return Quantity * Price;
        }

        public void DisplayProductInfo()
        {
            Console.WriteLine(
                $"ID: {ID} | " +
                $"Name: {Name} | " +
                $"Quantity: {Quantity} | " +
                $"Price: {Price:N2} kr"
            );
        }
    }
}