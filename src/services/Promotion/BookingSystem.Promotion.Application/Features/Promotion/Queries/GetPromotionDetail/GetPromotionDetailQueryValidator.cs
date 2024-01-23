using FluentValidation;

namespace BookingSystem.Promotion.Application.Features.Promotion.Queries.GetPromotionDetail;

public class GetPromotionDetailQueryValidator : AbstractValidator<GetPromotionDetailQuery>
{
    public GetPromotionDetailQueryValidator()
    {
        RuleFor(x => x.PromotionId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Must be enter promotion Id.")
            .GreaterThan(0).WithMessage("Promotion Id must be greater then zero.");
    }
}
