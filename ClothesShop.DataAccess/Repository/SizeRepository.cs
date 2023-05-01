using ClothesShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothesShop.DataAccess.Repository.IRepository;

namespace ClothesShop.DataAccess.Repository
{
    public class SizeRepository : Repository<Size>, ISizeRepository
    {
        public SizeRepository(ApplicationDbContext context) : base(context) { }
        public void Update(Size size)
        {
            _context.Set<Size>().Update(size);
        }
    }
}
