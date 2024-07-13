using AdminSide.Models;
using System.Text.Json;

namespace AdminSide.Services
{
    internal static class AdminManager
    {
        private static  Random rand = new Random();
        public static List<Admin> Admins { get; set; } = new List<Admin>();
        public static List<Category> Categories { get; set; } = new List<Category>();
        public static List<Product> Products { get; set; } = new List<Product>();

        public static List<ReportElement> Reports { get; set; } = new List<ReportElement>();
        public static Admin? Admin { get; set; }

        static AdminManager()
        {

            if (File.Exists("reports.json"))
            {
                var json = File.ReadAllText("reports.json");
                var ListOfReports = JsonSerializer.Deserialize<List<ReportElement>>(json);
                if (ListOfReports != null) Reports = ListOfReports;
            }
            Reports ??= [];

            if (File.Exists("products.json"))
            {
                var json = File.ReadAllText("products.json");
                var ListOfProducts = JsonSerializer.Deserialize<List<Product>>(json);
                if (ListOfProducts != null) Products = ListOfProducts;
            }
            Products ??= [];

            if (File.Exists("categories.json"))
            {
                var json = File.ReadAllText("categories.json");
                var ListOfCategories = JsonSerializer.Deserialize<List<Category>>(json);
                if(ListOfCategories != null) Categories = ListOfCategories;
            }
            Categories ??= [];

            if (File.Exists("admins.json"))
            {
                var json = File.ReadAllText("admins.json");
                var ListOfAdmins = JsonSerializer.Deserialize<List<Admin>>(json);
                if (ListOfAdmins != null) Admins = ListOfAdmins;
            }
        }
        
        public static void Login(string email,string password)
        {
            Admin = Admins.FirstOrDefault(a => a.Email.Trim().ToString() == email && a.Password == password);
            if (Admin == null) throw new Exception("Invalid email or password");
        }

        public static void LogOut()
        {
            Admin = null;
        }

        public static void AddCategory(string name, int id)
        {
            Category category = new Category(name,id);
            Categories.Add(category);
            var json_category = JsonSerializer.Serialize(AdminManager.Categories);
            File.WriteAllText("categories.json", json_category);
        }

        public static void AddProduct(string name,string description,int quantity,string category,double price)
        {
            Product product = new Product(name,description,category,price,quantity);
            Products.Add(product);
            var json_prod = JsonSerializer.Serialize(AdminManager.Products);
            File.WriteAllText("products.json", json_prod);
            Category? category_ = Categories.FirstOrDefault(c => c.Name == product.Name);
            if (category_ != null) { category_.Products!.Add(product); }
            else
            {
                Category category_new = new Category()
                {
                    Name = product.Category,
                    Id = rand.Next(0,100)
                };
                Categories.Add(category_new);
                var json_category = JsonSerializer.Serialize(AdminManager.Categories);
                File.WriteAllText("categories.json", json_category);
            }
        }

        public static void UpdateTheProduct(string name,string newDescription,double newPrice,string newCategory)
        {
            var product = Products.FirstOrDefault(p => p.Name == name);
            if (product == null) throw new Exception("Product not found");
            product.Description = newDescription;
            product.Price = newPrice;
            product.Category = newCategory;
            var json_prod = JsonSerializer.Serialize(AdminManager.Products);
            File.WriteAllText("products.json", json_prod);
        }

        public static void ShowReports()
        {
            Console.Clear();
            foreach (var report in AdminManager.Reports) 
            {
                report.ShowInfo();
            }
        }

        public static void ShowStatistics()
        {

        }
    }
}
