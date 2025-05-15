using FluentValidation;

namespace RakSport.Application.Commands.UserPanel.Reservation.CreateReview
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.ReservationId).NotEmpty()
                .WithMessage("Reservation Id is required.");

            RuleFor(x => x.CleanLinessRating)
                .NotEmpty().WithMessage("CleanLinessRating is required.")
                .InclusiveBetween(1, 5).WithMessage("The score should be between 1 and 5.");

            RuleFor(x => x.StaffBehaviorRating)
                .NotEmpty().WithMessage("StaffBehaviorRating is required.")
                .InclusiveBetween(1, 5).WithMessage("The score should be between 1 and 5.");

            RuleFor(x => x.PriceToQualityRating)
                .NotEmpty().WithMessage("PriceToQualityRating is required.")
                .InclusiveBetween(1, 5).WithMessage("The score should be between 1 and 5.");

            RuleFor(x => x.VentilationLightingRating)
                .NotEmpty().WithMessage("VentilationLightingRating is required.")
                .InclusiveBetween(1, 5).WithMessage("The score should be between 1 and 5.");

            RuleFor(x => x.LockerRoomRating)
                .NotEmpty().WithMessage("LockerRoomRating is required.")
                .InclusiveBetween(1, 5).WithMessage("The score should be between 1 and 5.");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Comment is required.")
                .MinimumLength(5).WithMessage("Your comment must be at least 5 characters long.")
                .MaximumLength(200).WithMessage("Your comment must be a maximum of 200 characters.");
        }
    }
}
