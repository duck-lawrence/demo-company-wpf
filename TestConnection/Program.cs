using DataAccessLayer.AppDbContext;

namespace TestConnection
{
    public class Program
    {
        static void Main(string[] args)
        {
            var db = new CompanyDbContext();
            foreach (var item in db.Accounts.ToList())
            {
                Console.WriteLine(item.Id);
            }
            Console.WriteLine("Hello, World!");
        }
    }
}