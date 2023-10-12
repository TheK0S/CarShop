using System.ComponentModel.DataAnnotations;

namespace CarShop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public int AccessLevel { get; set; }
    }
}
