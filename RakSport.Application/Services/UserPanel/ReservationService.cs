using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using RakSport.Application.Commands.UserPanel.Reservation;
using RakSport.Application.DTOs.UserPanel.Reservation;
using RakSport.Application.Interfaces.UserPanel;
using RakSport.Domain.Interfaces.User.Rservation;
using RakSport.Domain.Models.Reservation;
using RakSport.Domain.Models.SportHall;
using RakSport.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RakSport.Application.Services.UserPanel
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository, UserManager<UserEntity> userManager)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<GetReservationByIdDto> GetReservationByIdAsync(ClaimsPrincipal User, GetReservationByIdCommand command)
        {
            //throw new NotImplementedException();

            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(loggedInUserId))
                throw new UnauthorizedAccessException();

            var userId = Guid.Parse(loggedInUserId);

            var reservation = await _reservationRepository.GetReservationWithDetailsByIdAsync(userId, command.ReservationId);

            if (reservation == null)
                throw new Exception("Rservation not found");

            var sportHall = reservation.ReservationItems.OrderBy(x => x.TimeSlot.StartTime).First().TimeSlot.SportHall;

            var firstTimeSlot = reservation.ReservationItems.OrderBy(t => t.TimeSlot.StartTime).First().TimeSlot;

            var lastTimeSlot = reservation.ReservationItems.OrderByDescending(t => t.TimeSlot.EndTime).First().TimeSlot;

            return new GetReservationByIdDto
            {
                HallName = sportHall.Title,

                HallImageUrl = sportHall.Images.FirstOrDefault()?.Url,

                AvreageRating = sportHall.reviews.Any() ? (float)sportHall.reviews.Average(r => r.Rating) : 0f,

                TotalReviews = sportHall.reviews.Count(),

                TotalSessions = reservation.ReservationItems.Count(),

                SessionGenderType = reservation.ReservationItems.First().TimeSlot.gender.ToString(),

                StartDate = DateTime.Now.Add(reservation.ReservationItems.Min(t => t.TimeSlot.StartTime)),

                EndDate = DateTime.Now.Add(reservation.ReservationItems.Min(t => t.TimeSlot.EndTime)),

                Status = reservation.Status.ToString(),

                TrackingCode = reservation.Payment.GetWayTrackingCode,

                CanExtend = reservation.Status == ReservationEntity.ReservationStatus.Paid && reservation.ReservationItems != null && reservation.ReservationItems.Any()

                && reservation.CreateAt.Date.Add(reservation.ReservationItems.Max(x => x.TimeSlot.EndTime)) > DateTime.Now,

                CanReview = reservation.Status == ReservationEntity.ReservationStatus.Paid && reservation.ReservationItems != null && reservation.ReservationItems.Any()

                && reservation.CreateAt.Date.Add(reservation.ReservationItems.Max(x => x.TimeSlot.EndTime)) > DateTime.Now && !reservation.IsReviewed,
            };
        }
    }
}
