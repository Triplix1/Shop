using ClothesShop.DataAccess;
using ClothesShop.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.DataAccess.Repository
{
    public class UnitOfWork :IUnitOfWork
    {
        ApplicationDbContext _context;
        public ICategoryRepository Categories { get; set; }
        public IProductRepository Products { get; set; }

        public UnitOfWork(ICategoryRepository categoryRepository, IProductRepository productRepository, ApplicationDbContext context)
        {
            Categories = categoryRepository;
            _context = context;
            Products = productRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
