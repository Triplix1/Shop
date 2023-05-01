using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClothesShop.Models.ViewModels
{
    public class ProductVM
    {
        public IEnumerable<SelectListItem>? CategoriesList { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Price should be positive value")]
        public decimal Price { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public List<Size> Sizes { get; set; }

        [Required]
        public IFormFile MainPhoto { get; set; }
        public string? MainImageUrl { get; set; }

        [Required]
        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryModel>? Gallery { get; set; }
    }
}
