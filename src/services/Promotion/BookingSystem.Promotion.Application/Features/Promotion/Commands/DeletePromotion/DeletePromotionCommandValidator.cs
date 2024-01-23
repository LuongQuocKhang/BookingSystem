using FluentValidation;

namespace BookingSystem.Promotion.Application.Features.Promotion.Commands.DeletePromotion;

public class DeletePromotionCommandValidator : AbstractValidator<DeletePromotionCommand>
{
    public DeletePromotionCommandValidator()
    {
        RuleFor(x => x.PromotionId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Must enter Promotion id.");
    }
}
