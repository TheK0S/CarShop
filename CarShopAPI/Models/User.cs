using System.ComponentModel.DataAnnotations;

namespace CarShopAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public int RoleId { get; set; } = 2;
        public virtual Role? Role { get; set; }
    }
}
