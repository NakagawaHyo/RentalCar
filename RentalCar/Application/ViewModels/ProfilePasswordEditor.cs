using System.ComponentModel.DataAnnotations;

namespace RentalCar.Application.ViewModels
{
    public class ProfilePasswordEditor
    {
        [Required(ErrorMessage = "Current Password is required")]
        [Display(Name = "現在のパスワード")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        [Display(Name = "新しいパスワード")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("NewPassword", ErrorMessage = "The New Password and Confirm Password do not match.")]
        [Display(Name = "新しいパスワード(確認)")]
        public string ConfirmPassword { get; set; }
    }
}
