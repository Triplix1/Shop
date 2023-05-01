using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.Models
{
    public class Size
    {
        public int Id { get; set; }

        public string? InString { get; set; }

        public float? InNumber { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
