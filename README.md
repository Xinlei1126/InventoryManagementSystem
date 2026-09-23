# InventoryManagementSystem

Individual project.

# Inventory Management System

A C# .NET console application for managing product inventory and storing inventory data between program runs.

The application lets users add, update, delete, and view products through an interactive terminal menu. Product data is saved automatically to a JSON file, and users can generate a formatted inventory report showing quantities, prices, and the total inventory value.

## Features

- Add products with an ID, name, quantity, and price
- Prevent duplicate product IDs
- Update existing product information
- Delete products with confirmation before removal
- View all products in a formatted console table
- Sort displayed products by product ID
- Calculate the total price of each product (`Quantity × Price`)
- Calculate the total quantity of all products
- Calculate the total value of the inventory
- Save inventory data automatically to `inventory.json`
- Load saved inventory data automatically when the application starts
- Generate a formatted inventory report
- Optionally save the report to `InventoryReport.txt`
- Validate product names, quantities, IDs, and prices
- Display success messages in green and error messages in red

## Requirements

Before running the application, make sure you have the following installed:

- Git
- .NET 10 SDK

You can check whether .NET is installed by opening a terminal and running:

```bash
dotnet --version
```

The installed version should be 10.0 or later.

## Installation

### 1. Clone the repository

Open a terminal and run:

```bash
git clone https://github.com/Xinlei1126/InventoryManagementSystem.git
```

### 2. Navigate to the project

```bash
cd InventoryManagementSystem/InventoryManagementSystem/InventoryManagementSystem
```

### 3. Restore the project

Run:

```bash
dotnet restore
```

This restores any dependencies required by the project.

## Running the Application

From the `InventoryManagementSystem/InventoryManagementSystem` project directory, run:

```bash
dotnet run
```

The application will start in the terminal and display the main menu:

```text
======================================
    INVENTORY MANAGEMENT SYSTEM
======================================

========== MAIN MENU ==========
1. Add Product
2. Update Product
3. Delete Product
4. View All Products
5. Generate Report
6. Exit
===============================
```

Enter a number from `1` to `6` to choose an action.

## How to Use

### 1. Add a product

Choose option `1` from the main menu.

The application asks for:

- Product ID
- Product name
- Quantity
- Price

Example:

```text
===== ADD PRODUCT =====
Enter Product ID: 1
Enter Product Name: iPhone 13
Enter Quantity: 10
Enter Price: 9879
Product added successfully!
```

Each product ID must be unique. IDs, quantities, and prices must be non-negative values, and the product name cannot be empty.

After a product is added, the inventory is automatically saved to `inventory.json`.

### 2. Update a product

Choose option `2` and enter the ID of the product you want to update.

The application displays the current product information and asks for a new name, quantity, and price.

```text
===== UPDATE PRODUCT =====
Enter Product ID to update: 1

Current Product:
ID: 1 | Name: iPhone 13 | Quantity: 10 | Price: 9,879.00 kr

Enter New Product Name: iPhone 13
Enter New Quantity: 15
Enter New Price: 9500
Product updated successfully!
```

The updated inventory is saved automatically.

### 3. Delete a product

Choose option `3` and enter the product ID.

Before deleting the product, the application displays its information and asks for confirmation:

```text
Are you sure you want to delete this product? (Y/N):
```

Enter `Y` to delete the product. Any other response cancels the deletion.

### 4. View all products

Choose option `4` to display all products.

Products are sorted by ID and displayed in a formatted table similar to:

```text
              ===== ALL PRODUCTS =====

Number  ID  Name         Quantity        Price
----------------------------------------------
1       1   iPhone 13          10  9,879.00 kr
2       2   iPad mini           5  8,768.00 kr
----------------------------------------------
```

### 5. Generate an inventory report

Choose option `5` to generate a report.

The report includes:

- Product number
- Product ID
- Product name
- Quantity
- Unit price
- Total price for each product
- Total quantity of all products
- Total inventory value

Example:

```text
                 ===== INVENTORY REPORT =====

Number  ID  Name         Quantity        Price      Total Price
---------------------------------------------------------------
1       1   iPhone 13         324  9,879.00 kr  3,200,796.00 kr
2       2   iPad mini         345  8,768.00 kr  3,024,960.00 kr
3       4   AirPods 4          67  1,988.00 kr    133,196.00 kr
---------------------------------------------------------------
Total                         736               6,358,952.00 kr
---------------------------------------------------------------
```

After displaying the report, the application asks:

```text
Save report to file? (Y/N):
```

Enter `Y` to save the report as:

```text
InventoryReport.txt
```

The application also displays the full location of the saved report file.

### 6. Exit the application

Choose option `6` to close the program.

```text
Thank you for using the Inventory Management System.
```

## Data Storage

The application uses JSON file storage instead of a database.

Inventory data is saved in:

```text
inventory.json
```

A saved product looks similar to:

```json
{
  "ID": 1,
  "Name": "iPhone 13",
  "Quantity": 10,
  "Price": 9879
}
```

The file is loaded automatically when a new `Inventory` object is created, so products remain available between application runs.

## Project Structure

```text
InventoryManagementSystem/
├── README.md
└── InventoryManagementSystem/
    ├── InventoryManagementSystem.slnx
    └── InventoryManagementSystem/
        ├── InventoryManagementSystem.csproj
        ├── Program.cs
        ├── Product.cs
        ├── Inventory.cs
        ├── inventory.json          # Created/updated at runtime
        └── InventoryReport.txt     # Created when a report is saved
```

### Main Files

- `Program.cs` — starts the application, displays the main menu, and handles menu selections
- `Product.cs` — represents a product with an ID, name, quantity, and price; also calculates the product's total value
- `Inventory.cs` — manages products, validation, JSON storage, tables, calculations, and report generation
- `InventoryManagementSystem.csproj` — contains the .NET project configuration and targets .NET 10
- `InventoryManagementSystem.slnx` — Visual Studio solution file
- `inventory.json` — stores product data between runs
- `InventoryReport.txt` — optional generated text report containing inventory totals

## Quick Start

For users who already have Git and .NET 10 installed:

```bash
git clone https://github.com/Xinlei1126/InventoryManagementSystem.git
cd InventoryManagementSystem/InventoryManagementSystem/InventoryManagementSystem
dotnet restore
dotnet run
```

## Technologies

- C#
- .NET 10
- LINQ
- `System.Text.Json`
- File I/O
- Object-Oriented Programming
- Git
- GitHub

## About

Individual project for building an Inventory Management System with C# and .NET.
