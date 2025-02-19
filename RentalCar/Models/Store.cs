using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Hosting;

namespace RentalCar.Models
{
    public class Store : BaseModel
    {

        [Display(Name = "店舗名")]
        [Required(ErrorMessage = "{0}は必須です")]
        public string Name { get; set; }

        [Display(Name = "備考")]
        public string? Description { get; set; }

        [Display(Name = "郵便番号")]
        [Required(ErrorMessage = "{0}は必須です")]
        public string ZipCode { get; set; }

        [Display(Name = "住所1")]
        [Required(ErrorMessage = "{0}は必須です")]
        public string Address1 { get; set; }

        [Display(Name = "住所2")]
        public string? Address2 { get; set; }

        public bool ? IsDeleted { get; set; } = false;
        public ICollection<User> Users { get; } = new List<User>();
    }
}
