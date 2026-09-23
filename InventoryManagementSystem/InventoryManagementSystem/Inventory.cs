using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace InventoryManagementSystem
{
    internal class Inventory
    {
        private List<Product> products =
            new List<Product>();

        private readonly string filePath =
            "inventory.json";

        public Inventory()
        {
            LoadFromFile();
        }

        // =========================================
        // ADD PRODUCT
        // =========================================
        public void AddProduct()
        {
            Console.WriteLine(
                "\n===== ADD PRODUCT ====="
            );

            int id =
                ReadInt("Enter Product ID: ");

            if (products.Any(p => p.ID == id))
            {
                ShowError(
                    "A product with this ID already exists."
                );

                return;
            }

            string name =
                ReadProductName(
                    "Enter Product Name: "
                );

            int quantity =
                ReadInt(
                    "Enter Quantity: "
                );

            decimal price =
                ReadDecimal(
                    "Enter Price: "
                );

            Product newProduct =
                new Product(
                    id,
                    name,
                    quantity,
                    price
                );

            products.Add(newProduct);

            SaveToFile();

            ShowSuccess(
                "Product added successfully!"
            );
        }

        // =========================================
        // UPDATE PRODUCT
        // =========================================
        public void UpdateProduct()
        {
            Console.WriteLine(
                "\n===== UPDATE PRODUCT ====="
            );

            int id =
                ReadInt(
                    "Enter Product ID to update: "
                );

            Product? product =
                products.FirstOrDefault(
                    p => p.ID == id
                );

            if (product == null)
            {
                ShowError(
                    "Product not found."
                );

                return;
            }

            Console.WriteLine(
                "\nCurrent Product:"
            );

            product.DisplayProductInfo();

            Console.WriteLine();

            string name =
                ReadProductName(
                    "Enter New Product Name: "
                );

            int quantity =
                ReadInt(
                    "Enter New Quantity: "
                );

            decimal price =
                ReadDecimal(
                    "Enter New Price: "
                );

            product.Name = name;
            product.Quantity = quantity;
            product.Price = price;

            SaveToFile();

            ShowSuccess(
                "Product updated successfully!"
            );
        }

        // =========================================
        // DELETE PRODUCT
        // =========================================
        public void DeleteProduct()
        {
            Console.WriteLine(
                "\n===== DELETE PRODUCT ====="
            );

            int id =
                ReadInt(
                    "Enter Product ID to delete: "
                );

            Product? product =
                products.FirstOrDefault(
                    p => p.ID == id
                );

            if (product == null)
            {
                ShowError(
                    "Product not found."
                );

                return;
            }

            Console.WriteLine(
                "\nProduct to delete:"
            );

            product.DisplayProductInfo();

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write(
                "\nAre you sure you want to delete this product? (Y/N): "
            );

            Console.ResetColor();

            string choice =
                (Console.ReadLine() ?? "")
                .Trim()
                .ToUpper();

            if (choice != "Y")
            {
                Console.WriteLine(
                    "Delete cancelled."
                );

                return;
            }

            products.Remove(product);

            SaveToFile();

            ShowSuccess(
                "Product deleted successfully!"
            );
        }

        // =========================================
        // VIEW PRODUCTS
        // =========================================
        public void ViewProducts()
        {
            if (products.Count == 0)
            {
                ShowError(
                    "Inventory is empty."
                );

                return;
            }

            string table =
                CreateProductTable(
                    false,
                    false,
                    out int tableWidth
                );

            Console.WriteLine();

            PrintCenteredTitle(
                "===== ALL PRODUCTS =====",
                tableWidth
            );

            Console.WriteLine();

            Console.Write(
                table
            );
        }

        // =========================================
        // GENERATE REPORT
        // =========================================
        public void GenerateReport()
        {
            if (products.Count == 0)
            {
                ShowError(
                    "Inventory is empty."
                );

                return;
            }

            string table =
                CreateProductTable(
                    true,
                    true,
                    out int tableWidth
                );

            Console.WriteLine();

            PrintCenteredTitle(
                "===== INVENTORY REPORT =====",
                tableWidth
            );

            Console.WriteLine();

            Console.Write(
                table
            );

            Console.Write(
                "\nSave report to file? (Y/N): "
            );

            string choice =
                (Console.ReadLine() ?? "")
                .Trim()
                .ToUpper();

            if (choice == "Y")
            {
                SaveReport();
            }
        }

        // =========================================
        // CREATE TABLE
        // =========================================
        private string CreateProductTable(
            bool includeTotalPrice,
            bool includeTotalRow,
            out int totalWidth)
        {
            List<Product> sortedProducts =
                products
                    .OrderBy(p => p.ID)
                    .ToList();

            int numberWidth =
                Math.Max(
                    "Number".Length,
                    sortedProducts
                        .Count
                        .ToString()
                        .Length
                ) + 2;

            int idWidth =
                Math.Max(
                    "ID".Length,
                    sortedProducts
                        .Max(
                            p =>
                                p.ID
                                .ToString()
                                .Length
                        )
                ) + 2;

            int nameWidth =
                Math.Max(
                    "Name".Length,
                    sortedProducts
                        .Max(
                            p =>
                                p.Name.Length
                        )
                ) + 2;

            int quantityWidth =
                Math.Max(
                    "Quantity".Length,
                    sortedProducts
                        .Max(
                            p =>
                                p.Quantity
                                .ToString()
                                .Length
                        )
                ) + 2;

            int priceWidth =
                Math.Max(
                    "Price".Length,
                    sortedProducts
                        .Max(
                            p =>
                                $"{p.Price:N2} kr"
                                .Length
                        )
                ) + 2;

            int totalPriceWidth = 0;

            if (includeTotalPrice)
            {
                totalPriceWidth =
                    Math.Max(
                        "Total Price".Length,
                        sortedProducts
                            .Max(
                                p =>
                                    $"{p.GetTotalPrice():N2} kr"
                                    .Length
                            )
                    ) + 2;
            }

            totalWidth =
                numberWidth +
                idWidth +
                nameWidth +
                quantityWidth +
                priceWidth +
                totalPriceWidth;

            StringBuilder table =
                new StringBuilder();

            // Header
            table.Append(
                "Number"
                    .PadRight(numberWidth)
            );

            table.Append(
                "ID"
                    .PadRight(idWidth)
            );

            table.Append(
                "Name"
                    .PadRight(nameWidth)
            );

            table.Append(
                "Quantity"
                    .PadLeft(quantityWidth)
            );

            table.Append(
                "Price"
                    .PadLeft(priceWidth)
            );

            if (includeTotalPrice)
            {
                table.Append(
                    "Total Price"
                        .PadLeft(totalPriceWidth)
                );
            }

            table.AppendLine();

            table.AppendLine(
                new string(
                    '-',
                    totalWidth
                )
            );

            int number = 1;

            // Product rows
            foreach (Product product in sortedProducts)
            {
                table.Append(
                    number
                        .ToString()
                        .PadRight(numberWidth)
                );

                table.Append(
                    product.ID
                        .ToString()
                        .PadRight(idWidth)
                );

                table.Append(
                    product.Name
                        .PadRight(nameWidth)
                );

                table.Append(
                    product.Quantity
                        .ToString()
                        .PadLeft(quantityWidth)
                );

                table.Append(
                    $"{product.Price:N2} kr"
                        .PadLeft(priceWidth)
                );

                if (includeTotalPrice)
                {
                    table.Append(
                        $"{product.GetTotalPrice():N2} kr"
                            .PadLeft(totalPriceWidth)
                    );
                }

                table.AppendLine();

                number++;
            }

            // Total row
            if (includeTotalRow)
            {
                table.AppendLine(
                    new string(
                        '-',
                        totalWidth
                    )
                );

                int totalQuantity =
                    CalculateTotalQuantity();

                decimal totalValue =
                    CalculateTotalInventoryValue();

                table.Append(
                    "Total"
                        .PadRight(numberWidth)
                );

                table.Append(
                    ""
                        .PadRight(idWidth)
                );

                table.Append(
                    ""
                        .PadRight(nameWidth)
                );

                table.Append(
                    totalQuantity
                        .ToString()
                        .PadLeft(quantityWidth)
                );

                table.Append(
                    ""
                        .PadLeft(priceWidth)
                );

                table.Append(
                    $"{totalValue:N2} kr"
                        .PadLeft(totalPriceWidth)
                );

                table.AppendLine();
            }

            table.AppendLine(
                new string(
                    '-',
                    totalWidth
                )
            );

            return table.ToString();
        }

        // =========================================
        // CALCULATE TOTAL QUANTITY
        // =========================================
        private int CalculateTotalQuantity()
        {
            return products.Sum(
                p => p.Quantity
            );
        }

        // =========================================
        // CALCULATE TOTAL INVENTORY VALUE
        // =========================================
        private decimal CalculateTotalInventoryValue()
        {
            return products.Sum(
                p => p.GetTotalPrice()
            );
        }

        // =========================================
        // SAVE REPORT
        // =========================================
        private void SaveReport()
        {
            string reportPath =
                "InventoryReport.txt";

            string table =
                CreateProductTable(
                    true,
                    true,
                    out int tableWidth
                );

            string title =
                "===== INVENTORY REPORT =====";

            int leftPadding =
                Math.Max(
                    0,
                    (tableWidth - title.Length) / 2
                );

            using StreamWriter writer =
                new StreamWriter(reportPath);

            writer.WriteLine(
                new string(
                    ' ',
                    leftPadding
                ) + title
            );

            writer.WriteLine();

            writer.Write(
                table
            );

            ShowSuccess(
                "\nReport saved successfully!"
            );

            Console.WriteLine(
                $"Location: {Path.GetFullPath(reportPath)}"
            );
        }

        // =========================================
        // SAVE INVENTORY TO JSON
        // =========================================
        private void SaveToFile()
        {
            try
            {
                JsonSerializerOptions options =
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };

                string json =
                    JsonSerializer.Serialize(
                        products,
                        options
                    );

                File.WriteAllText(
                    filePath,
                    json
                );
            }
            catch (Exception)
            {
                ShowError(
                    "Could not save inventory file."
                );
            }
        }

        // =========================================
        // LOAD INVENTORY FROM JSON
        // =========================================
        private void LoadFromFile()
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            try
            {
                string json =
                    File.ReadAllText(
                        filePath
                    );

                products =
                    JsonSerializer
                        .Deserialize<List<Product>>(
                            json
                        )
                    ?? new List<Product>();
            }
            catch (Exception)
            {
                products =
                    new List<Product>();

                ShowError(
                    "Could not load inventory file."
                );
            }
        }

        // =========================================
        // PRODUCT NAME INPUT
        // =========================================
        private string ReadProductName(
            string message)
        {
            while (true)
            {
                Console.Write(
                    message
                );

                string name =
                    (Console.ReadLine() ?? "")
                    .Trim();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    return name;
                }

                ShowError(
                    "Product name cannot be empty."
                );
            }
        }

        // =========================================
        // INTEGER INPUT
        // =========================================
        private int ReadInt(
            string message)
        {
            while (true)
            {
                Console.Write(
                    message
                );

                string input =
                    Console.ReadLine() ?? "";

                if (
                    int.TryParse(
                        input,
                        out int value
                    )
                    &&
                    value >= 0
                )
                {
                    return value;
                }

                ShowError(
                    "Please enter a valid " +
                    "non-negative whole number."
                );
            }
        }

        // =========================================
        // DECIMAL INPUT
        // =========================================
        private decimal ReadDecimal(
            string message)
        {
            while (true)
            {
                Console.Write(
                    message
                );

                string input =
                    Console.ReadLine() ?? "";

                if (
                    decimal.TryParse(
                        input,
                        out decimal value
                    )
                    &&
                    value >= 0
                )
                {
                    return value;
                }

                ShowError(
                    "Please enter a valid " +
                    "non-negative price."
                );
            }
        }

        // =========================================
        // SUCCESS MESSAGE
        // =========================================
        private void ShowSuccess(
            string message)
        {
            Console.ForegroundColor =
                ConsoleColor.Green;

            Console.WriteLine(
                message
            );

            Console.ResetColor();
        }

        // =========================================
        // ERROR MESSAGE
        // =========================================
        private void ShowError(
            string message)
        {
            Console.ForegroundColor =
                ConsoleColor.Red;

            Console.WriteLine(
                message
            );

            Console.ResetColor();
        }

        // =========================================
        // CENTER TITLE
        // =========================================
        private void PrintCenteredTitle(
            string title,
            int tableWidth)
        {
            int leftPadding =
                Math.Max(
                    0,
                    (tableWidth - title.Length) / 2
                );

            Console.WriteLine(
                new string(
                    ' ',
                    leftPadding
                ) + title
            );
        }
    }
}