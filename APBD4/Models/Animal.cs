using System.ComponentModel.DataAnnotations;

namespace APBD4.Models
{
    public class Animal
    {
        [Required(ErrorMessage = "Name cant be empty")]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Category cant be empty")]
        [MaxLength(100)]
        public string Category { get; set; }
        [Required(ErrorMessage = "Area cant be empty")]
        [MaxLength(100)]
        public string Area { get; set; }
    }
}
