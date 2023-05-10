using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Xml.Linq;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

           var repo = new DapperDepartmentRepository(conn);

           Console.WriteLine("Type a new Department Name");
           var newDepartment = Console.ReadLine();

           repo.InsertDepartment(newDepartment);

            Console.WriteLine("-----------------------------------------------------------");

            var departments = repo.GetAllDepartments();

           foreach (var dept in departments) 
           {
               Console.WriteLine($"Departments: {dept.DepartmentID}-{dept.Name}");
           }
           
            Console.WriteLine("-----------------------------------------------------------");
            
            var repo2 = new DapperProductRepository(conn);


            var products = repo2.GetAllProducts();
            foreach (var prod in products) 
            {
                Console.WriteLine($"Products: {prod.ProductID}-{prod.Name}-{prod.Price}-{prod.CategoryID}");
            }

            repo2.CreateProduct("USBA Adapters", 5.50, 4);

            repo2.UpdateProduct(2, "Lenovo You-Go!", 999.00, 1,0,"25");

            repo2.DeleteProduct(2);   //Does not work at this point but is correct on a basic level.

            repo2.InsertProduct("USBC Adapters");
           
        }
    }
}
