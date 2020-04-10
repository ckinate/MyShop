using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
   public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }
        public void Commit()
        {
            cache["products"] = products;
        }
        public void Insert(Product p)
        {
            products.Add(p);
        }
        public void Update(Product productItem)
        {
            Product ProductToUpdate = products.Find(p => p.Id == productItem.Id);
            if(ProductToUpdate != null)
            {
                ProductToUpdate = productItem;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        public Product LookFor(string Id)
        {
            Product productItems = products.Find(p => p.Id == Id);
            if (productItems != null)
            {
                return productItems;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void ProductDelete(string Id)
        {
            Product ProductToDelete = products.Find(p => p.Id == Id);
            if (ProductToDelete != null)
            {
                products.Remove(ProductToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
