using System.ComponentModel.DataAnnotations;

namespace CarShop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [StringLength(15,MinimumLength = 8)]
        public string? Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [StringLength(50)]
        public string? Email { get; set; }
        public DateTime Created { get; set; }
        public int RoleId { get; set; } = 2;
        public virtual Role? Role { get; set; }
    }
}
