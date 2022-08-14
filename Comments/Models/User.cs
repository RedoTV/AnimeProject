
using System.ComponentModel.DataAnnotations;

namespace Animes.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не введён логин")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Не введён пароль")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        public string? Role { get; set; }

    }
}
