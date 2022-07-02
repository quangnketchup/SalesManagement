using DataAccess.DataAccess;
using DataAccess.DataProvider;
using DataAccess.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO : BaseDAL
    {
        //-----------------------
        //using singleton
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        //-----------------------
        public IEnumerable<Product> getProductList()
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select ProductId, CategoryId, ProductName, Weight, UnitPrice, UnitInStock from Product";
            var products = new List<Product>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    products.Add(new Product
                    {
                        ProductId = dataReader.GetInt32(0),
                        CategoryId = dataReader.GetInt32(1),
                        ProductName = dataReader.GetString(2),
                        Weight = dataReader.GetString(3),
                        UnitPrice = dataReader.GetDecimal(4),
                        UnitslnStock = dataReader.GetInt32(5)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return products;
        }


        //-----------------------
        public Product getProductByID(int ProductId)
        {
            Product product = null;
            IDataReader dataReader = null;
            string SQLSelect = "Select ProductId, CategoryId, ProductName, Weight, UnitPrice, UnitInStock from Product" +
                " Where ProductId = @ProductId";
            try
            {
                var param = dataProvider.CreateParameter("@ProductId", 4, ProductId, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    product = new Product
                    {
                        ProductId = dataReader.GetInt32(0),
                        CategoryId = dataReader.GetInt32(1),
                        ProductName = dataReader.GetString(2),
                        Weight = dataReader.GetString(3),
                        UnitPrice = dataReader.GetDecimal(4),
                        UnitslnStock = dataReader.GetInt32(5)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return product;
        }

        //-----------------------
        public void addNewProduct(Product product)
        {
            try
            {
                Product pro = getProductByID(product.ProductId);
                if (pro == null)
                {
                    String SQLInsert = "Insert Product values (@ProductId, @CategoryId, @ProductName, @Weight, @UnitPrice, @UnitInStock)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@ProductId", 4, product.ProductId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@CategoryId", 4, product.CategoryId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@ProductName", 50, product.ProductName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Weight", 50, product.Weight, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@UnitPrice", 50, product.UnitPrice, DbType.Decimal));
                    parameters.Add(dataProvider.CreateParameter("@UnitInStock", 4, product.UnitslnStock, DbType.Int32));
                    dataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("Product existed!!!");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        //-----------------------
        public void updateProduct(Product product)
        {
            try
            {
                Product pro = getProductByID(product.ProductId);
                if (pro != null)
                {
                    String SQLInsert = "Update Product set CategoryId= @CategoryId, ProductName = @ProductName," +
                        " Weight =@Weight, UnitPrice = @UnitPrice, UnitInStock = @UnitInStock  where ProductId = @ProductId";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@ProductId", 4, product.ProductId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@CategoryId", 4, product.CategoryId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@ProductName", 50, product.ProductName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Weight", 50, product.Weight, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@UnitPrice", 50, product.UnitPrice, DbType.Decimal));
                    parameters.Add(dataProvider.CreateParameter("@UnitInStock", 4, product.UnitslnStock, DbType.Int32));
                    dataProvider.Update(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("No Product existed!!!");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        //-------------------------God help me
        public void removeProduct(int productId)
        {
            try
            {
                IOrderDetailRepository OrderDetailRepository = new OrderDetailRepository();
                var orderDetails = OrderDetailRepository.GetOrderDetailByProductId(productId);
                if (orderDetails.Count() == 0)
                {
                    String SQLInsert = "Delete Product where ProductId = @ProductId";
                    var param = dataProvider.CreateParameter("@ProductId", 4, productId, DbType.Int32);
                    dataProvider.Delete(SQLInsert, CommandType.Text, param);
                }
                else
                {
                    throw new Exception("Product is in use!!!");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        //filter the product list
        public List<Product> GetFilteredProduct(String tag)
        {
            List<Product> filtered = new List<Product>();
            foreach (Product product in getProductList())
            {
                int add = 0;
                if (product.ProductId.ToString().Contains(tag))
                    add = 1;
                if (product.ProductName.Contains(tag))
                    add = 1;
                if (product.UnitPrice.ToString().Contains(tag))
                    add = 1;
                if (product.UnitslnStock.ToString().Contains(tag))
                    add = 1;
                if (add == 1)
                    filtered.Add(product);
            }
            return filtered;
        }
    }
}
