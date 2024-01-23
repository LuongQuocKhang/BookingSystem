using FluentValidation;

namespace BookingSystem.Stay.Application.Features.Commands.Stay.DeleteStay;

public class DeleteStayCommandValidator : AbstractValidator<DeleteStayCommand>
{
    public DeleteStayCommandValidator()
    {
        RuleFor(x => x.StayId)
            .GreaterThan(0).WithMessage("Stay Id must be greater than 0.")
            .NotEmpty();
    }
}
