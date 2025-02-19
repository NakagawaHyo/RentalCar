using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RentalCar.Models
{
    public class CarType : BaseModel
    {
        [Display(Name = "車種名")]
        [Required(ErrorMessage = "{0}は必須です")]
        public string Name { get; set; }

        [Display(Name = "クラス")]
        [Required(ErrorMessage = "{0}は必須です")]
        public int CarClassId { get; set; }

        [ValidateNever]
        [Display(Name = "クラス")]
        public CarClass CarClass { get; set; }

        [Display(Name = "説明")]
        public string? Description { get; set; }

        [Display(Name = "備考")]
        public string? Remarks { get; set; }

        [Display(Name = "表示順")]
        [Required(ErrorMessage = "{0}は必須です")]
        public int? DisplayOrder { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
