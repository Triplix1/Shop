using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0,int.MaxValue, ErrorMessage = "Price should be positive value")]
        public decimal Price { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string MainPhotoUrl { get; set; }

        [Required]
        public ICollection<ProductGallery> ImagesUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public List<Size> Sizes { get; set; }
    }
}
