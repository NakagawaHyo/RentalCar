using Spark.Library.Database;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.Application.Models
{
    public class CarClass : BaseModel
    {
        [Display(Name = "クラス名")]
        public string Name { get; set; }

        [Display(Name = "分類")]
        public int CarCategoryId { get; set; }

        [Display(Name = "分類")]
        public CarCategory carCategory { get; set; }

        [Display(Name = "説明")]
        public string? Description { get; set; }

        [Display(Name = "備考")]
        public string? Remarks { get; set; }

        [Display(Name= "表示順")]
        public int DisplayOrder { get; set; }
    }

    public class CarCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
