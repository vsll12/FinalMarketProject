using Final.Models;
using System.Data.SqlTypes;
using System.Numerics;
using System.Text.Json;
using UserSide.Models;

namespace Final.Services
{
    internal static class UserManager
    {
        public static List<Category> Categories { get; set; } = new List<Category>();
        public static List<User> Users { get; set; } = new List<User>();
        public static User? User { get; set; }
        public static List<ReportElement> Reports { get; set; } = new List<ReportElement>();
        public static double? Total { get; set; } = 0;

        static UserManager()
        {
            if (File.Exists("users.json"))
            {
                var json = File.ReadAllText("users.json");
                var ListOfUsers = JsonSerializer.Deserialize<List<User>>(json);
                if(ListOfUsers is not null)Users = ListOfUsers;
            }
            Users ??= [];

            if (File.Exists("C:\\Users\\DELL\\Desktop\\C#Final\\FinalMarket\\Final\\AdminPanel\\bin\\Debug\\net8.0\\categories.json"))
            {
                var json = File.ReadAllText("C:\\Users\\DELL\\Desktop\\C#Final\\FinalMarket\\Final\\AdminPanel\\bin\\Debug\\net8.0\\categories.json");
                var ListOfCategories = JsonSerializer.Deserialize<List<Category>>(json);
                if (ListOfCategories != null) Categories = ListOfCategories;
            }
            Categories ??= [];
        }

        public static void Register(string name, string surname, string email, string password, string date)
        {
            var user = Users.FirstOrDefault(u => u.Email == email);
            if (user is null)
            {
                user = new User()
                {
                    Email = email,
                    Password = password,
                    DateOfBirth = DateOnly.ParseExact(date, "dd.MM.yyyy"),
                    Name = name,
                    Surname = surname
                };
                Users.Add(user);

                var json = JsonSerializer.Serialize(Users);
                File.WriteAllText("users.json", json);

                return;
            }
            throw new Exception("User has already exist");
        }

        public static void LogIn(string email, string password)
        {
            User = Users.FirstOrDefault(u => u.Email == email.ToLower().Trim() && u.Password == password);
            if (User is null) throw new Exception("Invalid email or password");
            User.BasketElements = new List<BasketElement>();
        }

        public static void LogOut()
        {
            User = null;
        }

        public static void AddProductToBasket(string name , int quantity ,  double price)
        {
            if(User is not null)
            {
                var BasketElement = new BasketElement(name,quantity,price);
                User.BasketElements.Add(BasketElement);
                Total += price;
                return;
            }
            throw new Exception("User is not found");
        }

        public static void ShowBasket()
        {
            //if (User is not null && User.BasketElements is not null)
            //{
                int i = 0;
                foreach (var BasketElement in User!.BasketElements!)
                {
                    //if(BasketElement.ProductName is not null)

                    Console.WriteLine($"{i + 1}. {BasketElement.ProductName}  {BasketElement?.Quantity}");
                    i++;
                }
                return;
            //}
            //throw new Exception("Something is null here");
        }

        public static void RemoveTheProductFromBasket(string name)
        {
            //if (User?.BasketElements is not null)
            //{
                foreach (var item in User?.BasketElements!)
                {
                    if (item.ProductName == name)
                    {
                        User.BasketElements.Remove(item);
                        Total -= item.Price;
                        return;
                    }
                }
                //return;
            //}
        }
    }
}
