using System.ComponentModel.DataAnnotations;

namespace RentalCar.Application.ViewModels
{
	public class Register
	{
		[Required(ErrorMessage = "Email is required")]
		[Display(Name = "ログインID")]
		public string LoginId { get; set; }

		[Required(ErrorMessage = "Name is required")]
		[Display(Name = "名前")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[Display(Name = "パスワード")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm password is required")]
		[Compare("Password", ErrorMessage = "The Password and Confirm Password do not match.")]
		[Display(Name = "パスワード(確認)")]
		public string ConfirmPassword { get; set; }
	}
}
