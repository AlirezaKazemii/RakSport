using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakSport.Application.Commands.UserPanel.Reservation
{
    public class GetReservationByIdCommand
    {
        public Guid ReservationId { get; set; }
    }
}
