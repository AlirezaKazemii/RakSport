using RakSport.Domain.Models.TimeSlot;
using RakSport.Infrastructure.DbContext;
using RakSport.Infrastructure.Repositories.Genericrepository;
using RakSport.Infrastructure.Repositories.UserPanel.TimeSlotRepostiory;

namespace RakSport.Infrastructure.Repositories.UserPanel.timeSlotRepostiory
{
    public class TimeSlotRepository : GenericRepository<TimeSlotEntity> , ITimeSlotRepository
    {
        private readonly DataBaseContext _context;

        public TimeSlotRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
