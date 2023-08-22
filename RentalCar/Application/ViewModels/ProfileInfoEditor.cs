using System.ComponentModel.DataAnnotations;

namespace RentalCar.Application.ViewModels
{
    public class ProfileInfoEditor
    {
        [Required]
        [Display(Name = "名前")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Required(ErrorMessage = "Please enter a valid email address")]
        [Display(Name="メールアドレス")]
        public string Email { get; set; }
    }
}
