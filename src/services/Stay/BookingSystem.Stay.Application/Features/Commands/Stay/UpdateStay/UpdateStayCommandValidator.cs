using FluentValidation;

namespace BookingSystem.Stay.Application.Features.Commands.Stay.UpdateStay;

public class UpdateStayCommandValidator : AbstractValidator<UpdateStayCommand>
{
    public UpdateStayCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Stay Id must be greater than 0.")
            .NotEmpty();
    }
}
