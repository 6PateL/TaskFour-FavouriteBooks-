using System.ComponentModel.DataAnnotations;

namespace TaskFour_FavouriteBooks_.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+\.[A-Za-z]{2,4}", ErrorMessage = "Enter valid email")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
