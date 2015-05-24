using Longgan.DataAccess.Home;
using Longgan.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longgan.Logics.Home
{
    public class ProductsLogic : Logic
    {
        ProductsDA da = new ProductsDA();
        public List<Product> GetProducts()
        {
            return da.GetProducts();
        }

        public Product GetProduct(string Id)
        {
            return da.GetProduct(Id);
        }

        public void UpdateProduct(Product n)
        {
            da.UpdateProduct(n);
        }

        public void AddProduct(Product n)
        {
            n.Id = Guid.NewGuid().ToString();
            n.Created = DateTime.Now;
            da.AddProduct(n);
        }

        public void RemoveProduct(Product n)
        {
            da.RemoveProduct(n);
        }
    }
}
