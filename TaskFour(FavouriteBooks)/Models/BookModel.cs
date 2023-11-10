using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFour_FavouriteBooks_.Models
{
    public class BookModel
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [Required]
        public byte[] Image { get; set; }
        public string FilePath { get; set; }
        [Required]
        public string Name { get; set; }
        [Required] 
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        public User User { get; set; }
    }
}
