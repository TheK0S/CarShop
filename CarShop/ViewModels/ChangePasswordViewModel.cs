using System.ComponentModel.DataAnnotations;

namespace CarShop.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display]
        [StringLength(15,MinimumLength = 5)]
        public string NewPassword { get; set; }
    }
}
