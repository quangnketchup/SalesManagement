using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetFilteredProducts(String tag);
        Product GetProductByID(int ProductID);
        void InsertProduct(Product Product);
        void DeleteProduct(int ProductID);
        void UpdateProduct(Product Product);
        List<Product> GetProductByUnitPriceAndUnitInStock(int a, int b);
    }
}
