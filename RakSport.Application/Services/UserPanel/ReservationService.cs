using RakSport.Application.Commands.UserPanel.Reservation;
using RakSport.Application.Commands.UserPanel.Reservation.CancelReservtaion;
using RakSport.Application.Commands.UserPanel.Reservation.ExtendReservation;
using RakSport.Application.Commands.UserPanel.Reservation.GetAllReservations;
using RakSport.Application.Commands.UserPanel.Reservation.GetReservationById;
using RakSport.Application.Commands.UserPanel.Reservation.GetReservationSessions;
using RakSport.Application.DTOs.UserPanel.Reservation;
using RakSport.Application.Interfaces.UserPanel;
using RakSport.Domain.Interfaces.GenericInterface;
using RakSport.Domain.Interfaces.User.Reservation;
using RakSport.Domain.Interfaces.UserPanel.ReservationItem;
using RakSport.Domain.Models.Reservation;
using RakSport.Domain.Models.ReservationItem;
using RakSport.Domain.Models.Review;
using RakSport.Infrastructure.Repositories.UserPanel.TimeSlotRepostiory;
using System.Globalization;
using System.Security.Claims;
using static RakSport.Domain.Models.Reservation.ReservationEntity;

namespace RakSport.Application.Services.UserPanel
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IGenericRepository<ReviewEntity> _reviewRepository;
        private readonly IReservationItemRepository _reservationItem;
        private readonly ITimeSlotRepository _timeSlotRepository;

        public ReservationService(
            IReservationRepository reservationRepository,
            IGenericRepository<ReviewEntity> ReviewRepository,
            IReservationItemRepository reservationItem,
            ITimeSlotRepository timeSlotRepository
            )
        {
            _reservationRepository = reservationRepository;
            _reviewRepository = ReviewRepository;
            _reservationItem = reservationItem;
            _timeSlotRepository = timeSlotRepository;
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

        public async Task<ReservationSessionsResultDto> GetReservationSessionsAsync(GetReservationSessionsCommand command, ClaimsPrincipal User)
        {
            var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedInUser is null)
                throw new UnauthorizedAccessException();

            var userId = Guid.Parse(loggedInUser);

            var reservaion = await _reservationRepository.GetReservationWithDetailsByIdAsync(userId, command.ReservationId);

            if (reservaion is null)
                throw new Exception("Reservation not found");

            var sessionList = reservaion.ReservationItems.ToList();

            var sessionListDto = new List<ReservationSessionsDto>();

            foreach (var session in sessionList)
            {
                if (session.IsCancelled)
                    continue;

                var status = (session.Date < DateTime.Now) ? "Completed" : "Uncoming";

                var sessionDto = new ReservationSessionsDto()
                {
                    Date = session.Date,
                    Price = session.Price,
                    TimeRange = $"{session.TimeSlot.StartTime:hh\\:mm} - {session.TimeSlot.EndTime:hh\\:mm}",
                    Status = status
                };
                sessionListDto.Add(sessionDto);
            }

            return new ReservationSessionsResultDto
            {
                TotalSessions = sessionListDto.Count,
                CompletedSessions = sessionListDto.Count(x => x.Status == "Completed"),
                RemainingSessions = sessionListDto.Count(x => x.Status == "Uncoming"),
                Sessions = sessionListDto.OrderBy(x => x.Date).ToList()
            };
        }

        public async Task<ExtendReservationDto> ExtendReservationAsync(ExtendReservatonCommand command, ClaimsPrincipal User)
        {
            // دریافت رزرو و اعتبار سنجی
            var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedInUser is null)
                throw new UnauthorizedAccessException();

            var userId = Guid.Parse(loggedInUser);

            var reservation = await _reservationRepository.GetReservationWithDetailsByIdAsync(userId,command.ReservationId);

            if (reservation is null)
                throw new Exception("Reservation not found!");

            if (reservation.Status != ReservationStatus.Paid)
                throw new Exception("Only paid reservations can be extended.");

            if (reservation.Type != ReservationType.Monthly)
                throw new Exception("The selected reservation cannot be extended.");

            // واکشی الگوی رزرو فعلی یا جلسات قبلی
            var reservationItems = reservation.ReservationItems;

            var weeklyPattern = reservationItems
                .Select(item => new
                {
                    item.TimeSlot.Date.DayOfWeek,
                    item.TimeSlot.StartTime,
                    item.TimeSlot.EndTime,
                    item.TimeSlot.SportHallId,
                    item.TimeSlotId,
                    item.Price
                })
                .Distinct()
                .ToList();
                

            //ساخت جلسات جدید
            var newReservationItems = new List<ReservationItemEntity>();
            var startDate = reservationItems.Max(x => x.Date).AddDays(1); //شروع از ماه بعد

            for(int i = 0; i<command.MonthCount; i++)
            {
                var monthStart = startDate.AddMonths(i);
                // گرفتن تمام تاریخ‌های ماه فعلی
                var datesInMonth = GetDatesForMonth(monthStart);

                foreach(var pattern in weeklyPattern)
                {
                    // فقط تاریخ‌هایی که روز هفته‌شون با الگوی رزرو قبلی می‌خونه
                    var matchDates = datesInMonth
                        .Where(d => d.Date.DayOfWeek == pattern.DayOfWeek)
                        .ToList();

                    foreach(var date in matchDates)
                    {
                        // بررسی اینکه این سانس در این تاریخ خالی هست یا نه
                        var isAvailable = await _reservationItem.IsTimeSlotAvailable(pattern.TimeSlotId,date);

                        if (!isAvailable)
                            throw new Exception($"تاریخ {date:yyyy/MM/dd} قبلاً رزرو شده و قابل تمدید نیست.");

                        // ساخت جلسه جدید                        
                        newReservationItems.Add(new ReservationItemEntity(date, pattern.TimeSlotId, pattern.Price));
                        
                    }
                }                
            }

            //ساخت رزرو جدید
            var newReservation = new ReservationEntity(
                userId,
                newReservationItems.Sum(x => x.Price),
                ReservationStatus.Paid,
                ReservationType.Monthly
            );

            foreach (var item in newReservationItems)
            {
                //item.SetReservation(newReservation);
                //reservation.ReservationItems.Add(item);
                newReservation.AddItem(item);
                item.SetTimeSlot(await _timeSlotRepository.GetByIdAsync(item.TimeSlotId));
            }

            await _reservationRepository.AddAsync(newReservation);
           
            var reservationItemsDto = new List<ReservationItemDto>();

            foreach(var item in newReservationItems)
            {
                var newItem = new ReservationItemDto()
                {
                    Date = item.Date.ToString("yyyy/MM/dd"),
                    dayOfWeek = item.Date.DayOfWeek,
                    StartTime = item.TimeSlot.StartTime,
                    EndTime = item.TimeSlot.EndTime,
                    Price = item.Price,
                };
                reservationItemsDto.Add(newItem);
            }

            return new ExtendReservationDto()
            {
                ReservationId = newReservation.Id,
                SessionCount = newReservationItems.Count,
                //StartDate = newReservationItems.Min(x => x.Date).ToString("yyyy/MM/dd"),
                //EndDate = newReservationItems.Max(x => x.Date).ToString("yyyy/MM/dd"),
                TotalAmount = newReservationItems.Sum(x => x.Price),
                ReservationItems = reservationItemsDto
            };

        }

        public static List<DateTime> GetDatesForMonth(DateTime anyDateInTargetMonth)
        {
            var calendar = new PersianCalendar();
            var dates = new List<DateTime>();

            int year = calendar.GetYear(anyDateInTargetMonth);
            int month = calendar.GetMonth(anyDateInTargetMonth);

            int daysInMonth;
            if (month < 6)
                daysInMonth = 31;
            else if (month <= 12)
                daysInMonth = 30;
            else 
                daysInMonth = calendar.IsLeapYear(year) ? 30 : 29;

            for(int day = 1; day <= daysInMonth; day++)
            {
                var date = calendar.ToDateTime(year, month, day, 0, 0, 0, 0);
                dates.Add(date);
            }

            return dates;
        }
    }
}
