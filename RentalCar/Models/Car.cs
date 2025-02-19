using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RentalCar.Models
{
    public class Car : BaseModel
    {
        [Display(Name = "クラス")]
        public int CarClassId { get; set; } = 1;

        [ValidateNever]
        [Display(Name = "クラス")]
        public CarClass CarClass { get; set; }

        [Display(Name = "区分")]
        public int CarDivisionId { get; set; }

        [ValidateNever]
        [Display(Name = "区分")]
        public CarDivision CarDivision { get; set; }

        [Display(Name = "車種")]
        public int CarTypeId { get; set; }

        [ValidateNever]
        [Display(Name = "車種")]
        public CarType CarType { get; set; }

        [Display(Name = "管轄店舗")]
        public int StoreId { get; set; }

        [ValidateNever]
        [Display(Name = "管轄店舗")]
        public Store Store { get; set; }

        [Display(Name = "車番")]
        [Required(ErrorMessage = "{0}は必須です")]
        public string CarNumber { get; set; }

        [Display(Name = "説明")]
        public string? Description { get; set; }

        [Display(Name = "備考")]
        public string? Remarks { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
