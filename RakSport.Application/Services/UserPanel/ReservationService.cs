using RakSport.Application.Commands.UserPanel.Reservation;
using RakSport.Application.Commands.UserPanel.Reservation.CancelReservtaion;
using RakSport.Application.Commands.UserPanel.Reservation.GetAllReservations;
using RakSport.Application.Commands.UserPanel.Reservation.GetReservationById;
using RakSport.Application.DTOs.UserPanel.Reservation;
using RakSport.Application.Interfaces.UserPanel;
using RakSport.Domain.Interfaces.GenericInterface;
using RakSport.Domain.Interfaces.User.Reservation;
using RakSport.Domain.Models.Reservation;
using RakSport.Domain.Models.Review;
using System.Security.Claims;
using static RakSport.Domain.Models.Reservation.ReservationEntity;

namespace RakSport.Application.Services.UserPanel
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IGenericRepository<ReviewEntity> _reviewRepository;

        public ReservationService(IReservationRepository reservationRepository, IGenericRepository<ReviewEntity> ReviewRepository)
        {
            _reservationRepository = reservationRepository;
            _reviewRepository = ReviewRepository;
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

                AvreageRating = sportHall.reviews.Any() ? (float)reservation.Review.AverageRating : 0f,

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

                if (hall.reviews?.Any() == true)
                {
                    averageRating = (float)reservation.Review?.AverageRating;
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

        public async Task CreateReviewAsunc(CreateReviewCommand command, ClaimsPrincipal User)
        {
            var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedInUser is null)
                throw new UnauthorizedAccessException();

            var userId = Guid.Parse(loggedInUser);

            var reservation = await _reservationRepository.GetReservationWithDetailsByIdAsync(userId, command.ReservationId);

            if (reservation is null)
                throw new Exception("Reservation no found");

            if (reservation.UserId != userId)
                throw new Exception("The reservation does not belong to you.");

            if (reservation.IsReviewed)
                throw new Exception("You have already commented on this reservation.");

            if (reservation.Status != ReservationStatus.Paid)
                throw new Exception("Comments can only be left for paid reservations.");

            //var sportHallId = reservation.ReservationItems.FirstOrDefault().TimeSlot.SportHallId;

            var firstItem = reservation.ReservationItems.FirstOrDefault();

            if (firstItem == null)
                throw new Exception("First item is null");

            if (firstItem.TimeSlot == null)
                throw new Exception("First time slot is null");

            var sportHallId = firstItem.TimeSlot.SportHallId;

            var review = new ReviewEntity(
                sportHallId,
                userId,
                reservation.Id,
                command.CleanLinessRating,
                command.PriceToQualityRating,
                command.StaffBehaviorRating,
                command.LockerRoomRating,
                command.VentilationLightingRating,
                command.Comment);

            await _reviewRepository.AddAsync(review);

            reservation.Reviewed();

            await _reservationRepository.UpdateAsync(reservation);
        }

        public async Task<CancelReservationResultDto> CancelReservationAsync(CancelReservationCommand command, ClaimsPrincipal User)
        {
            var LoggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (LoggedInUser is null)
                throw new UnauthorizedAccessException();

            var userId = Guid.Parse(LoggedInUser);

            var reservation = await _reservationRepository.GetReservationWithDetailsByIdAsync(userId, command.ReservationId);

            if (reservation is null)
                throw new Exception("رزرو یافت نشد");

            if (reservation.Status != ReservationStatus.Paid)
                throw new Exception("فقط رزروهای پرداخت ‌شده قابل لغو هستند");

            if (command.ItemsIdsToCancel == null || !command.ItemsIdsToCancel.Any())
                throw new Exception("هیچ جلسه‌ای برای لغو انتخاب نشده");

            var itemsToCancel = reservation.ReservationItems
                .Where(i => command.ItemsIdsToCancel.Contains(i.Id))
                .ToList();

            if (!itemsToCancel.Any())
                throw new Exception("هیچ جلسه‌ای برای لغو انتخاب نشده");

            var allItems = reservation.ReservationItems.ToList();

            var refundPolicy = allItems.Select(i => i.TimeSlot.SportHall.RefundPolicy).FirstOrDefault();

            if (refundPolicy == null)
                throw new Exception("قوانین استرداد یافت نشد");

            decimal totalPenalty = 0;
            decimal totalRefund = 0;
            var cancelledList = new List<CancelledSessionDto>();

            foreach(var item in itemsToCancel)
            {
                var daysLeft = (item.TimeSlot.Date - DateTime.Now.Date).Days;

                int penaltyPercentage;
                string reason;

                if (daysLeft >= 3)
                {
                    penaltyPercentage = refundPolicy.OverThreeDaysPenalty;
                    reason = "بیش از ۳ روز مانده تا جلسه";
                }
                else if (daysLeft == 2)
                {
                    penaltyPercentage = refundPolicy.TwoDaysPenalty;
                    reason = "۲ روز مانده تا جلسه";
                }
                else if (daysLeft == 1)
                {
                    penaltyPercentage = refundPolicy.OneDayOrLessPenalty;
                    reason = "۱ روز یا کمتر مانده تا جلسه";
                }
                else
                    throw new Exception("امکان لغو کردن وجود ندارد");

                decimal penaltyAmount = Math.Round(item.Price * penaltyPercentage / 100, 0);
                decimal refundAmount = item.Price - penaltyAmount;

                item.Cancel();

                totalPenalty += penaltyAmount;
                totalRefund += penaltyAmount;

                cancelledList.Add(new CancelledSessionDto
                {
                    SessionId = item.Id,
                    OriginalPrice = item.Price,
                    PenaltyPercentage = penaltyPercentage,
                    RefundAmount = refundAmount
                });

            }

            if(reservation.ReservationItems.All(i => i.IsCancelled))
                reservation.setCancelled();                        

            return new CancelReservationResultDto
            {
                CancelledSessions = cancelledList,
                TotalPenalty = totalPenalty,
                TotalRefund = totalRefund
            };
        }
    }
}
