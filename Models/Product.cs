using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCWebApplication3.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        [ForeignKey("Category")]
        public int  CategoryId{ get; set; }

    }
}
