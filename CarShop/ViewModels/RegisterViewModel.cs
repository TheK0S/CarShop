using CarShop.Models;
using System.ComponentModel.DataAnnotations;

namespace CarShop.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "The name must be less than 20 characters long")]
        [MinLength(3, ErrorMessage = "The name must be more than 3 characters long")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [StringLength(50)]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage = "The password must be more than 8 characters long")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password", ErrorMessage = "Passwords is not equals")]
        public string PasswordConfirm { get; set; }
        public DateTime Created { get; set; }
        public int RoleId { get; set; } = 2;
        public virtual Role? Role { get; set; }

    }
}
