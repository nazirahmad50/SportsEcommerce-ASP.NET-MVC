using SportsEcommerce.Domain.Abstract;
using SportsEcommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsEcommerce.Domain.Concrete
{

    public class EFProductRespository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products => context.Products;

        public void SaveProduct(Product product)
        {
            // add a product to db
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            // update product in db
            else
            {
                Product dbEntry = context.Products.Find(product.ProductID);

                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;

                }
            }
            context.SaveChanges();
        }


        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Products.Find(productID);

            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }
    }
}
