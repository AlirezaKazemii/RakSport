using RakSport.Domain.Models.Reservation;
using RakSport.Domain.Models.SportHall;
using RakSport.Domain.Models.User;

namespace RakSport.Domain.Models.Review
{
    public class ReviewEntity
    {
        public Guid Id { get; private set; }
        public Guid SportHallId { get; private set; }
        public Guid UserId { get; private set; }
        public Guid ReservationId { get; set; }
        public int CleanLinessRating { get; private set; }  // کیفیت کف‌پوش و تمیزی
        public int PriceToQualityRating { get; set; }       // نسبت قیمت به کیفیت زمین
        public int StaffBehaviorRating { get; set; }        // برخورد پرسنل
        public int LockerRoomRating { get; set; }           // تمیزی رختکن
        public int VentilationLightingRating { get; set; }  // تهویه و نورپردازی
        public string Comment { get; private set; }
        public bool IsPublished { get; private set; }
        public DateTime CreateAt { get; private set; } 
        public double AverageRating => Math.Round((CleanLinessRating + PriceToQualityRating + StaffBehaviorRating + LockerRoomRating + VentilationLightingRating) / 5.0, 2);

        // Navigation Properties
        public UserEntity User { get; private set; }
        public SportHallEntity sportHall { get; private set; }
        public ReservationEntity Reservation { get; set; }



        public ReviewEntity(Guid SportHallId, Guid UserId, Guid ReservationId, int CleanLinessRating, int PriceToQualityRating , int StaffBehaviorRating, int LockerRoomRating, int VentilationLightingRating, string Comment)
        {
            Id = Guid.NewGuid();
            this.SportHallId = SportHallId;
            this.UserId = UserId;
            this.ReservationId = ReservationId;
            this.CleanLinessRating = CleanLinessRating;
            this.PriceToQualityRating = PriceToQualityRating;
            this.StaffBehaviorRating = StaffBehaviorRating;
            this.LockerRoomRating = LockerRoomRating;
            this.VentilationLightingRating = VentilationLightingRating;
            this.Comment = Comment;
            CreateAt = DateTime.Now;
            IsPublished = false;
        }

    }
}
