using ClothesShop.DataAccess;
using ClothesShop.DataAccess.Repository.IRepository;
using ClothesShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }
        public void Update(Product product, Product? origin = null)
        {

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            if (origin == null)
            {
                origin = _context.Products.FirstOrDefault(x => x.Id == product.Id);

                if (origin == null)
                {
                    throw new ArgumentException("Database doesn't contain this product");
                }
            }

            else
            {
                _context.Products.Attach(origin);
            }

            origin.Price = product.Price;
            origin.Category = product.Category;
            origin.Description = product.Description;
            origin.Name = product.Name;
            //origin.Sizes = product.Sizes;
            origin.ImagesUrl = product.ImagesUrl;

            _context.Update(origin);
        }
    }
}
