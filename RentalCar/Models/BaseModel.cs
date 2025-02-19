using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RentalCar.Models
{
    public interface IHasTimestamps
    {
        DateTime? UpdatedAt { get; set; }
        DateTime? CreatedAt { get; set; }
    }

    public class BaseModel :IHasTimestamps
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedAt { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? UpdatedAt { get; set; }
    }
}
