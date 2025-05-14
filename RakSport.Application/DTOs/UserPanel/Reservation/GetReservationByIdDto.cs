using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakSport.Application.DTOs.UserPanel.Reservation
{
    public class GetReservationByIdDto
    {
        public string HallName { get; set; }
        public string HallImageUrl { get; set; }
        public float AvreageRating { get; set; }
        public int TotalReviews { get; set; }

        public int TotalSessions { get; set; }
        public string SessionGenderType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Status { get; set; }
        public string TrackingCode { get; set; }

        public bool CanExtend { get; set; }
        public bool CanReview { get; set; }

    }
}
