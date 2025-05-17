
namespace RakSport.Domain.Models.MartialHall
{
    public class MartialHallEntity : SportHall.SportHallEntity
    {
        // طبق فیگمای طراحی شده فعلا هیچ پراپرتی اضافه‌ای ندارد
        public MartialHallEntity(Guid id, string title, Guid refundPolicyId, string description, int capacity, int durationMinutes, decimal basePrice, SportHallType type) : base(id, title, refundPolicyId, description, capacity, durationMinutes, basePrice, type)
        {
        }

        private MartialHallEntity()
        {
            
        }
    }
}
