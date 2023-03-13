using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please type the name!")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please type the display order")]
        [DisplayName("Display order")]
        [Range(1,100)]
        public int DisplayOrder { get; set; }
        [DisplayName("Created at")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
