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
    }
}
