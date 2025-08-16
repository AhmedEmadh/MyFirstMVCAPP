using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyFirstMVCAPP.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("The Price")]
        [Range(0, 1000000, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        
        [AllowNull]
        [DisplayName("Category")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? imagePath { get; set; }

        [NotMapped]
        public IFormFile? clientFile { get; set; }
    }
}
