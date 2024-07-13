using AdminSide.Models;
using AdminSide.Services;
using System.Text.Json;

Product product1 = new Product()
{
	Name = "p1",
	Description = "desc1",
	Price = 10,
	Quantity = 20,
	Category = "category1"
};


Product product2 = new Product()
{
    Name = "p2",
    Description = "desc2",
    Price = 5,
    Quantity = 30,
    Category = "category2"
};

Product product3 = new Product()
{
    Name = "p3",
    Description = "desc3",
    Price = 7,
    Quantity = 12,
    Category = "category2"
};

Product product4 = new Product()
{
    Name = "p4",
    Description = "desc4",
    Price = 12,
    Quantity = 40,
    Category = "category2"
};

Product product5 = new Product()
{
    Name = "p5",
    Description = "desc5",
    Price = 16,
    Quantity = 45,
    Category = "category1"
};

Category category1 = new Category()
{
    Name = "category1",
    Id = 1
};

Category category2 = new Category()
{
    Name = "category2",
    Id = 2
};

category1.Products?.Add(product1);
category1.Products?.Add(product5);

category2.Products?.Add(product2);
category2.Products?.Add(product3);
category2.Products?.Add(product4);

AdminManager.Categories.Add(category1);
AdminManager.Categories.Add(category2);

var json_category = JsonSerializer.Serialize(AdminManager.Categories);
File.WriteAllText("categories.json", json_category);

AdminManager.Products.Add(product1);
AdminManager.Products.Add(product2);
AdminManager.Products.Add(product3);
AdminManager.Products.Add(product4);
AdminManager.Products.Add(product5);

var json_prod = JsonSerializer.Serialize(AdminManager.Products);
File.WriteAllText("products.json", json_prod);

Admin admin1 = new Admin()
{
    Name = "admin1",
    Surname = "admin1",
    Email = "admin1@gmail.com",
    Password = "123",
};

 Admin admin2 = new Admin()
{
    Name = "admin2",
    Surname = "admin2",
    Email = "admin2@gmail.com",
    Password = "1234",
};

AdminManager.Admins.Add(admin1);
AdminManager.Admins.Add(admin2);

var json = JsonSerializer.Serialize(AdminManager.Admins);

File.WriteAllText("admins.json", json);

bool condition =  true;
while (condition)
{
	MenuCommand:
	Console.Clear();
    Console.WriteLine("1. Login");
    Console.WriteLine("2. Exit");
    Console.Write("Enter your choice : ");
    string choice = Console.ReadLine()!;
	switch (choice)
	{
		case "1":
			AdminPanel.LogIn();
			AdminPanel.MainMenu();
			break;
		case "2":
			condition = false;
			break;
		default:
            Console.WriteLine("Wrong choice");
			Thread.Sleep(2000);
			goto MenuCommand;
	}
}
