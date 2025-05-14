using RakSport.Domain.Interfaces.GenericInterface;
using RakSport.Domain.Models.RefreshToken;
using RakSport.Domain.Models.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakSport.Domain.Interfaces.User.Rservation
{
    public interface IReservationRepository : IGenericRepository<ReservationEntity>
    {
        Task<ReservationEntity?> GetReservationWithDetailsByIdAsync(Guid userId,Guid reservationId);
    }
}
