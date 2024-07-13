using Final.Helpers;
using Final.Models;
using System.Linq.Expressions;
using System.Text.Json;
using UserSide.Models;

namespace Final.Services
{
    internal static class UserPanel
    {

        public static void MainPage()
        {

            Console.Clear();
            Console.WriteLine("0 .Exit");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. LogIn");
        }

        public static void RegisterPage()
        {
        Register:
            Console.Clear();
            Console.Write("Name : ");
            var name = Console.ReadLine();
            Console.Write("Surname : ");
            var surname = Console.ReadLine();
            Console.Write("DateOfBirth (dd.MM.yyyy):");
            var date = Console.ReadLine();
            Console.Write("Email : ");
            var email = Console.ReadLine();
            Console.Write("Password : ");
            var password = Console.ReadLine();
            try
            {
                UserManager.Register(name!, surname!, email!.ToLower().Trim(), password!, date!);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(2000);
                goto Register;
            }
        }

        public static void LogInPage()
        {
        LogIn:
            Console.Clear();
            Console.Write("Email :");
            var email = Console.ReadLine();
            Console.Write("Password : ");
            var password = Console.ReadLine();

            try
            {
                UserManager.LogIn(email!, password!);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(2000);
                goto LogIn;
            }
        }

        public static void UserMenu()
        {
            bool condition = true;
            Console.Clear();
            while (condition)
            {

                if (UserManager.User is not null)
                {
                    UserMenu:
                    Console.Clear();
                    Console.WriteLine($"1. Categories");
                    Console.WriteLine($"2. Basket");
                    Console.WriteLine($"3. Profile");
                    Console.WriteLine($"4. Exit");
                    Console.Write("Enter your choice : ");
                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("0. Back");
                            for (int i = 0; i < UserManager.Categories?.Count(); i++)
                            {
                                Console.WriteLine($"{i + 1}. {UserManager.Categories[i].Name}");
                            }
                            Console.Write("Enter your choice : ");
                            int ch = int.Parse(Console.ReadLine()!);
                            if (ch == 0) { goto UserMenu; }
                            for (int i = 0; i < UserManager.Categories?.Count(); i++)
                            {
                                if (ch == i + 1)
                                {
                                Products:

                                    Console.Clear();
                                    Console.WriteLine("0. Back");
                                    for (int j = 0; j < UserManager.Categories![ch - 1].Products?.Count(); j++)
                                    {
                                        Console.Write($"{j + 1}. ");
                                        Console.WriteLine(UserManager.Categories![ch - 1].Products![j]!.ToString());
                                    }

                                    Console.Write("Enter the product number which you want to know about : ");
                                    int choice_prod = int.Parse(Console.ReadLine()!);

                                    if(choice_prod == 0)goto UserMenu;

                                    for (int k = 0; k < UserManager.Categories[ch - 1].Products?.Count(); k++)
                                    {
                                        if (choice_prod == k + 1)
                                        {
                                            UserManager.Categories![ch - 1]?.Products![choice_prod - 1]?.ShowInfo();
                                            Console.WriteLine();
                                            Console.WriteLine("0. Back");
                                            Console.WriteLine("1. Add to basket");
                                            Console.Write("Enter your choice : ");
                                            int input = int.Parse(Console.ReadLine()!);

                                            if (input == 0) { goto Products; }

                                            else if (input == 1)
                                            {
                                                quantity:
                                                Console.Clear();
                                                Console.Write("Enter the quantity of product : ");
                                                int quant = int.Parse(Console.ReadLine()!);
                                                try
                                                {
                                                    UserManager.AddProductToBasket(UserManager.Categories![ch - 1].Products![choice_prod - 1]!.Name!, quant, UserManager.Categories![ch - 1].Products![choice_prod - 1].Price);
                                                    ReportElement reportElement = new ReportElement(DateTime.Now, UserManager.Categories![ch - 1].Products![choice_prod - 1]!.Name, quant);
                                                    UserManager.Reports.Add(reportElement);

                                                    if (UserManager.Categories![ch - 1].Products![choice_prod - 1]!.Quantity - quant >= 0)
                                                    {
                                                        UserManager.Categories![ch - 1].Products![choice_prod - 1]!.Quantity -= quant;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("That much product doesn't exist");
                                                        Thread.Sleep(2000);
                                                        goto quantity;
                                                    }
                                                    goto Products;
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine(ex.Message);
                                                    Thread.Sleep(2000);
                                                    goto Products;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Wrong choice");
                                                Thread.Sleep(2000);
                                                Console.Clear();
                                                goto Products;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case "2":
                            //Basket
                            Console.Clear();
                            Console.WriteLine("0. Back to the UserMenu");
                            Console.WriteLine("1. Buy");
                            Console.WriteLine("2. Remove the element");
                            UserManager.ShowBasket();
                            Console.Write("Enter your choice : ");
                            var choice_p = int.Parse(Console.ReadLine()!);
                            if(choice_p == 0)
                            {
                                goto UserMenu;
                            }
                            else if (choice_p == 1)
                            {
                                Amount: 
                                Console.Clear();   
                                Console.WriteLine($"Total amount : {UserManager.Total}");
                                Console.Write("Enter the amount : ");
                                var amount = double.Parse(Console.ReadLine()!);
                                if(amount == UserManager.Total)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Thanks");
                                    Thread.Sleep(2000);
                                    return;
                                }
                                else if(amount > UserManager.Total)
                                {
                                    Console.Clear();
                                    Console.WriteLine($"Qaliq : {amount - UserManager.Total}");
                                    Thread.Sleep(2000);
                                }
                                else if(amount < UserManager.Total)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Wrong amount");
                                    Thread.Sleep(2000);

                                    goto Amount;
                                }

                            }
                            else if (choice_p == 2)
                            {
                                UserManager.ShowBasket();
                                Console.Write("Enter product name : ");
                                string name = Console.ReadLine();
                                UserManager.RemoveTheProductFromBasket(name);
                            }
                            break;
                        case "3":
                            //Profile
                            Console.WriteLine("1. Change name ");
                            Console.WriteLine("2. Change password ");
                            Console.WriteLine("3. History ");
                            Console.WriteLine("4. Log out ");
                            Console.Write("Enter your choice : ");
                            var prof_choice = int.Parse(Console.ReadLine()!);
                            if(prof_choice == 1)
                            {
                                ProfileManager.ChangeName();
                            }
                            else if (prof_choice == 2)
                            {
                                ProfileManager.ChangePassword();
                            }
                            else if (prof_choice == 3)
                            {
                                ProfileManager.History();
                            }
                            else if(prof_choice == 4)
                            {
                                ProfileManager.LogOut();
                            }
                            break;
                        case "4":
                            //Exit
                            condition = false;
                            //return;
                            var json = JsonSerializer.Serialize(UserManager.Reports);
                            File.WriteAllText("C:\\Users\\DELL\\Desktop\\C#Final\\FinalMarket\\Final\\AdminPanel\\bin\\Debug\\net8.0\\reports.json", json);
                            Console.Clear();
                            Console.WriteLine("...");
                            Thread.Sleep(2000);
                            break;
                        default:
                            Console.WriteLine("Wrong choice");
                            Thread.Sleep(2000);
                            Console.Clear();
                            goto UserMenu;
                            //break
                    }
                }
            }
        }
    }
}
