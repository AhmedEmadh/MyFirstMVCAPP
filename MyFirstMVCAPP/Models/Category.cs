using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstMVCAPP.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Disables identity
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public ICollection<Item>? Items { get; set; }
    }
}
