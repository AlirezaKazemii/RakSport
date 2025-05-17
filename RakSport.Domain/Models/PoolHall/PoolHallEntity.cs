
namespace RakSport.Domain.Models.PoolHall
{
    public class PoolHallEntity : SportHall.SportHallEntity
    {
        // طبق فیگمای طراحی شده فعلا هیچ پراپرتی اضافه‌ای ندارد        
        public PoolHallEntity(Guid id, string title, Guid refundPolicyId, string description, int capacity, int durationMinutes, decimal basePrice, SportHallType type) : base(id, title, refundPolicyId, description, capacity, durationMinutes, basePrice, type)
        {
        }

        private PoolHallEntity()
        {
            
        }
    }
}
