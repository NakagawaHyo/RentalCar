using System.ComponentModel.DataAnnotations;

namespace RentalCar.Models
{
    public class Reservation : BaseModel
    {
        [Display(Name = "車両")]
        public int CarId { get; set; }

        [Display(Name = "車両")]
        public Car Car { get; set; }

        [Display(Name = "借受人名")]
        public int LesseeId { get; set; }

        [Display(Name = "借受人名")]
        public Client Lessee { get; set; }

        [Display(Name = "運転者名")]
        public int? DriverId { get; set; }

        [Display(Name = "運転者名")]
        public Client? Driver { get; set; }

        [Required]
        [Display(Name = "出発日時")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }  // レンタル開始日時

        [Required]
        [Display(Name = "返却日時")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }  // レンタル終了日時

        [Required]
        [Display(Name = "貸渡状況")]
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

    }

    public enum ReservationStatus
    {
        Pending,     // 予約待機中
        Cancelled,   // 予約キャンセル
        InRental,    // 貸渡中（車両が借りられている状態）
        Returned     // 返却済（車両が返却された状態）
    }
}
