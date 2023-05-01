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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }

        public void Update(Category category, Category? origin = null)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            if (origin == null)
            {
                origin = _context.Categories.FirstOrDefault(x => x.Id == category.Id);

                if (origin == null)
                {
                    throw new ArgumentException("Database doesn't contain this category");
                }
            }
            else
            {
                _context.Categories.Attach(origin);
            }

            origin.CategoryName = category.CategoryName;
            _context.Update(origin);                
        }
        public new void Add(Category entity)
        {
            foreach (var size in entity.Sizes)
            {
                _context.Sizes.Add(size);
            }

            _context.Categories.Add(entity);
        }
    }
}
