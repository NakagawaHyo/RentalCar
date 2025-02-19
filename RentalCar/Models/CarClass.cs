using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RentalCar.Models
{
    public class CarClass : BaseModel
    {
        [Display(Name = "クラス名")]
        [Required(ErrorMessage = "{0}は必須です")]
        public string Name { get; set; }

        [Display(Name = "分類")]
        [Required(ErrorMessage = "{0}は必須です")]
        public int CarCategoryId { get; set; }

        [ValidateNever]
        [Display(Name = "分類")]
        public CarCategory CarCategory { get; set; }

        [Display(Name = "説明")]
        public string? Description { get; set; }

        [Display(Name = "備考")]
        public string? Remarks { get; set; }

        [Display(Name = "表示順")]
        [Required(ErrorMessage = "{0}は必須です")]
        public int? DisplayOrder { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<CarType> CarTypes { get; } = new List<CarType>();
    }
}
