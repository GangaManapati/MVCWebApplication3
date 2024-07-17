using System.ComponentModel.DataAnnotations;

namespace MVCWebApplication3.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Product>? Products { get; set; }
       
    }
}
