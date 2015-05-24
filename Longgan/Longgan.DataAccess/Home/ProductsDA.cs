using Longgan.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longgan.DataAccess.Home
{
    public class ProductsDA : GenericRepository<Product>
    {
        public List<Product> GetProducts()
        {
            return base.Get().OrderByDescending(p => p.Created).ToList();
        }

        public Product GetProduct(string Id)
        {
            return base.Get(p => p.Id == Id).FirstOrDefault();
        }

        public void UpdateProduct(Product n)
        {
            base.Update(n);
        }

        public void AddProduct(Product n)
        {
            base.Insert(n);
        }

        public void RemoveProduct(Product n)
        {
            base.Delete(n);
        }
    }
}
