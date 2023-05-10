using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO PRODUCTS (Name, Price, CategoryID) VALUES (@name, @price, @categoryID);", 
                new {name = name, price = price, categoryID = categoryID});
        }

        public void DeleteProduct(int prodID)
        {
            _connection.Execute("DELETE FROM PRODUCTS WHERE ProductID=@prodID;);",
                new {prodID=prodID});
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;").ToList();
        }

        public void InsertProduct(string newProductName)
        {
            _connection.Execute("INSERT INTO PRODUCTS (Name, Price, CategoryID) VALUES (@productName, @price, @categoryid);",
                new { productName = newProductName, price=1.00, categoryid=9  });
        }

        public void UpdateProduct(int productID, string name, double price, int categoryID, int onsale, string stock)
        {
            _connection.Execute("UPDATE PRODUCTS SET Name=@name, Price=@price, CategoryID=@categoryID, OnSale=@onsale, StockLevel=@stock WHERE ProductID=@productID;",
               new { productID=productID, name=name, price=price, categoryID=categoryID, onsale=onsale, stock=stock});
        }
    }
}
