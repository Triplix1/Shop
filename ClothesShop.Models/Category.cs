using System.ComponentModel.DataAnnotations;

namespace ClothesShop.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name has to be initialized")]
        [Display(Name = "Category name")]
        public string CategoryName { get; set; }

        [Required]
        public List<Size> Sizes { get; set; }
    }
}