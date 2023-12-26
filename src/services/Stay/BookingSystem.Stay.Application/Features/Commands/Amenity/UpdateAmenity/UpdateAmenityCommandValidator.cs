using FluentValidation;

namespace BookingSystem.Stay.Application.Features.Commands.Amenity.UpdateAmenity;

public class UpdateAmenityCommandValidator : AbstractValidator<UpdateAmenityCommand>
{
    public UpdateAmenityCommandValidator()
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Id must be greater than 0.")
            .NotEmpty()
            .NotNull();
    }
}
