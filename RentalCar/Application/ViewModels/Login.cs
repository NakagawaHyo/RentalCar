using System.ComponentModel.DataAnnotations;

namespace RentalCar.Application.ViewModels
{
	public class Login
	{
		[Required(ErrorMessage = "Email is required")]
		[Display(Name = "ログインID")]
		public string LoginId { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		[Display(Name = "パスワード")]
		public string Password { get; set; }

		public bool RememberMe { get; set; } = false;
	}
}
