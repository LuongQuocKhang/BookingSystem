using FluentValidation;
namespace BookingSystem.Stay.Application.Features.Commands.Amenity.DeleteAmenity;

public class DeleteAmenityCommandValidator : AbstractValidator<DeleteAmenityCommand>
{
    public DeleteAmenityCommandValidator() {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Id must be greater than 0.")
            .NotEmpty()
            .NotNull();
    }
}
