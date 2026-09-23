using System;
using InventoryManagementSystem;

Inventory inventory = new Inventory();

bool running = true;

Console.WriteLine(
    "======================================"
);

Console.WriteLine(
    "    INVENTORY MANAGEMENT SYSTEM"
);

Console.WriteLine(
    "======================================"
);

while (running)
{
    DisplayMenu();

    Console.Write(
        "\nSelect an option: "
    );

    string choice =
        (Console.ReadLine() ?? "")
        .Trim();

    switch (choice)
    {
        case "1":
            inventory.AddProduct();
            break;

        case "2":
            inventory.UpdateProduct();
            break;

        case "3":
            inventory.DeleteProduct();
            break;

        case "4":
            inventory.ViewProducts();
            break;

        case "5":
            inventory.GenerateReport();
            break;

        case "6":
            running = false;

            Console.WriteLine(
                "\nThank you for using " +
                "the Inventory Management System."
            );

            break;

        default:
            Console.ForegroundColor =
                ConsoleColor.Red;

            Console.WriteLine(
                "\nInvalid option. " +
                "Please choose 1-6."
            );

            Console.ResetColor();

            break;
    }
}


// =========================================
// DISPLAY MAIN MENU
// =========================================
void DisplayMenu()
{
    Console.WriteLine();

    Console.WriteLine(
        "========== MAIN MENU =========="
    );

    Console.WriteLine(
        "1. Add Product"
    );

    Console.WriteLine(
        "2. Update Product"
    );

    Console.WriteLine(
        "3. Delete Product"
    );

    Console.WriteLine(
        "4. View All Products"
    );

    Console.WriteLine(
        "5. Generate Report"
    );

    Console.WriteLine(
        "6. Exit"
    );

    Console.WriteLine(
        "==============================="
    );
}