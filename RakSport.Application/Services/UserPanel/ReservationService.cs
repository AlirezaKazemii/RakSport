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
