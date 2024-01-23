using FluentValidation;

namespace BookingSystem.Stay.Application.Features.Commands.Stay.CreateStay;

public class CreateStayCommandValidator : AbstractValidator<CreateStayCommand>
{
    public CreateStayCommandValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Must be enter stay name.")
            .NotNull();

        RuleFor(x => x.Address)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Must be enter stay Address.")
            .NotNull();

        RuleFor(x => x.PricePerNight)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Price Per Night must be greater then zero.");

        RuleFor(x => x.CheckInTime)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Must have Check In time.")
            .NotNull();

        RuleFor(x => x.CheckOutTime)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Must have Check Out time.")
            .NotNull();
    }
}
