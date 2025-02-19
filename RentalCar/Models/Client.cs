using System.ComponentModel.DataAnnotations;

namespace RentalCar.Models
{
    public class Client :BaseModel
    {
        [Display(Name = "名前")]
        [Required(ErrorMessage = "{0}は必須です")]
        public string Name { get; set; }

        [Display(Name = "メールアドレス")]
        [Required(ErrorMessage = "{0}は必須です")]
        public string Email { get; set; }

        [Display(Name = "電話番号")]
        [Required(ErrorMessage = "{0}は必須です")]
        public string Tel { get; set; }

        [Display(Name = "郵便番号")]
        [Required(ErrorMessage = "{0}は必須です")]
        public string ZipCode { get; set; }

        [Display(Name = "住所1")]
        [Required(ErrorMessage = "{0}は必須です")]
        public string Address1 { get; set; }

        [Display(Name = "住所2")]
        public string? Address2 { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
