using System.ComponentModel.DataAnnotations;

namespace ProductDTO
{
    public class ProductDT
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Category { get; set; }

    }
}