using Final.Models;
using Final.Services;
using System.Text.Json;

bool condition = true;

while (condition)
{
    if(UserManager.User is null)
    {
        MainPage:

        UserPanel.MainPage();

        Console.Write("Enter your choice : ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":

                UserPanel.RegisterPage();
                break;

            case "2":

                UserPanel.LogInPage();
                UserPanel.UserMenu();

                goto MainPage;

            case "0":

                condition = false;

                var json = JsonSerializer.Serialize(UserManager.Users);
                File.WriteAllText("C:\\Users\\DELL\\Desktop\\C#Final\\FinalMarket\\Final\\AdminPanel\\bin\\Debug\\net8.0\\users.json", json);

                var json_category = JsonSerializer.Serialize(UserManager.Categories);
                File.WriteAllText("C:\\Users\\DELL\\Desktop\\C#Final\\FinalMarket\\Final\\AdminPanel\\bin\\Debug\\net8.0\\categories.json", json_category);

                var json_report = JsonSerializer.Serialize(UserManager.Reports);
                File.WriteAllText("C:\\Users\\DELL\\Desktop\\C#Final\\FinalMarket\\Final\\AdminPanel\\bin\\Debug\\net8.0\\reports.json", json_report);

                break;

            default:
                Console.WriteLine("Wrong choice");
                Thread.Sleep(2000);
                Console.Clear();
                goto MainPage;
        }
    }
}
