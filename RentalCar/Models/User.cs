using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RentalCar.Models
{
    public class User : BaseModel
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        [Display(Name = "ログインID")]
        [Required(ErrorMessage = "{0}は必須です")]
        public string LoginId { get; set; }

        [Display(Name= "従業員名")]
        [Required(ErrorMessage = "{0}は必須です")]
        public string Name { get; set; }

        [Display(Name = "メールアドレス")]
        [Required(ErrorMessage = "{0}は必須です")]
        public string Email { get; set; }

        [Display(Name = "パスワード")]
        [Required(ErrorMessage = "{0}は必須です")]
        public string Password { get; set; }

        public bool IsDeleted { get; set; } = false;

        [Display(Name= "所属店舗")]
        [Required(ErrorMessage = "{0}は必須です")]
        public int StoreId { get; set; }

        [ValidateNever]
        public Store Store { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
