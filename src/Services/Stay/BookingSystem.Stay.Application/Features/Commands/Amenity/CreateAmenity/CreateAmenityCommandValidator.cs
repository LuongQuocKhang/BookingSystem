using FluentValidation;

namespace BookingSystem.Stay.Application.Features.Commands.Amenity.CreateAmenity;

public class CreateAmenityCommandValidator : AbstractValidator<CreateAmenityCommand>
{
    public CreateAmenityCommandValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Must be enter name.")
            .NotNull();

        RuleFor(x => x.Icon)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Must be enter icon.")
            .NotNull();
    }
}
