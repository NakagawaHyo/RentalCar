using System.ComponentModel.DataAnnotations;

namespace RentalCar.ViewModels
{
    public class Login
    {
        [Required(ErrorMessage = "{0}は必須です")]
        [Display(Name ="ログインID")]
        public string LoginId { get; set; } 

        [Required(ErrorMessage = "{0}は必須です")]
        [DataType(DataType.Password)]
        [Display(Name = "パスワード")]
        public string Password { get; set; }

        [Display(Name = "ログイン情報を保存する")]
        public bool RememberMe { get; set; } = false;
    }
}
