using freshie_DTO;

namespace tester_web_api
{
    internal class Program
    {

        //https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/rest

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var tester = new ProductTester();
            //List<Product> products = tester.GetAllProducts().Result;
            //List<Product> products = tester.GetProductsByCategory(3).Result;
            //products.ForEach(p=>Console.WriteLine(p.Name));
            Console.WriteLine("Podaj email");
            var email = "tester1@gmail.com"; //Console.ReadLine();
            Console.WriteLine("Podaj hasło");
            var password = "abcd1234";//Console.ReadLine();
            var usertester = new UserTester();
            var user = usertester.Login(email, password).Result;
            if(user!=null)
            {
                Console.WriteLine("Zalogowano użytkownika " + user.Name);
            }
            else { Console.WriteLine("Błędny użytkownik lub hasło"); }
            Console.ReadLine();
        }
    }
}