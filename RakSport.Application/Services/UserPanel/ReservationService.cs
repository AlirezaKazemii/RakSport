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
using RakSport.Application.Commands.UserPanel.Reservation;
using RakSport.Application.DTOs.UserPanel.Reservation;
using RakSport.Application.Interfaces.UserPanel;
using RakSport.Domain.Interfaces.User.Reservation;
using System.Security.Claims;
using static RakSport.Domain.Models.Reservation.ReservationEntity;

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
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

















































































        public async Task<List<GetAllReservationsDto>> GetAllAsync(GetAllReservationsCommand command, ClaimsPrincipal User)
        {

            var LoggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (LoggedInUserId is null)
                throw new UnauthorizedAccessException();

            var userId = Guid.Parse(LoggedInUserId);

            var reservations = await _reservationRepository.GetAllAsync(userId, command.Status);

            var reservationsDto = new List<GetAllReservationsDto>();

            foreach (var reservation in reservations)
            {
                var firstItem = reservation.ReservationItems?.FirstOrDefault();
                var timeSlot = firstItem?.TimeSlot;
                var hall = timeSlot?.SportHall;

                if (firstItem == null && timeSlot == null && hall == null)
                    continue;

                var hallName = hall.Title ?? "-";

                var imageUrl = hall.Images?.FirstOrDefault()?.Url ?? "";

                float averageRating = 0;

                if(hall.reviews?.Any() == true)
                {
                    averageRating = (float)Math.Round(hall.reviews.Average(r => r.Rating),1);
                }

                var totalReviews = hall.reviews?.Count ?? 0;
                var totalSessions = reservation.ReservationItems?.Count ?? 0;
                var sesstionGenderType = timeSlot.gender.ToString();
                var startDate = reservation.ReservationItems?.Min(x => x.TimeSlot?.Date ?? DateTime.MinValue) ?? DateTime.MinValue;
                var endDate = reservation.ReservationItems?.Max(x => x.TimeSlot?.Date ?? DateTime.MinValue) ?? DateTime.MinValue;
                var status = reservation.Status.ToString();
                var trackingCode = reservation.Payment?.TransactionId ?? "-";
                var canExtend = reservation.Status == ReservationStatus.Paid &&
                    reservation.ReservationItems.Max(i => i.TimeSlot.Date) < DateTime.Today;
                var canReview = reservation.Status == ReservationStatus.Paid &&
                    reservation.IsReviewed;

                var dto = new GetAllReservationsDto
                {
                    HallName = hallName,
                    HallImageUrl = imageUrl,
                    AverageRating = averageRating,
                    TotalReviews = totalReviews,
                    TotalSessions = totalSessions,
                    StartDate = startDate,
                    EndDate = endDate,
                    Status = status,
                    TrackingCode = trackingCode,
                    CanExtend = canExtend,
                    CanReview = canReview,
                };

                reservationsDto.Add(dto);
            }

            return reservationsDto;
        }
    }
}
