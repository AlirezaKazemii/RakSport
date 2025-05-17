using RakSport.Domain.Models.SportHall;

namespace RakSport.Domain.Models.RefundPolicy
{
    public class RefundPolicyEntity
    {
        public Guid Id { get; private set; }
        public int OverThreeDaysPenalty { get; private set; }     // برای ۳ روز یا بیشتر
        public int TwoDaysPenalty { get; private set; }           // برای ۲ روز مانده
        public int OneDayOrLessPenalty { get; private set; }      // برای ۱ روز یا کمتر
        public DateTime CreatedAt { get; private set; }

        //Navigation Properties
        public ICollection<SportHallEntity> SportHalls { get; private set; }


        public RefundPolicyEntity(Guid Id, int OverThreeDaysPenalty, int TwoDaysPenalty, int OneDayOrLessPenalty)
        {
            this.Id = Id;
            this.OverThreeDaysPenalty = OverThreeDaysPenalty;
            this.TwoDaysPenalty = TwoDaysPenalty;
            this.OneDayOrLessPenalty = OneDayOrLessPenalty;
            CreatedAt = DateTime.Now;
        }

        private RefundPolicyEntity()
        {
            
        }
    } 
}
