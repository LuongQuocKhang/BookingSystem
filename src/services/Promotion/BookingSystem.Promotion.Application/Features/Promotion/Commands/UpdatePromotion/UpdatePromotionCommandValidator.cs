using FluentValidation;

namespace BookingSystem.Promotion.Application.Features.Promotion.Commands.UpdatePromotion;

public class UpdatePromotionCommandValidator : AbstractValidator<UpdatePromotionCommand>
{
    public UpdatePromotionCommandValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Must enter name.");

        RuleFor(x => x.Description)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Must enter description.");

        RuleFor(x => x.DiscountType)
            .Cascade(CascadeMode.Stop)
            .Must(x => (int)x > -1).WithMessage("Must enter Discount Type.");

        RuleFor(x => x.FromDate)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Must enter From Date.")
            .GreaterThan(DateTime.Now).WithMessage("From Date must be before today")
            .Must((model, FromDate) =>
            {
                return FromDate < model.ToDate;
            }).WithMessage("From day must be before To Date");

        RuleFor(x => x.ToDate)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Must enter To Date.")
            .GreaterThan(DateTime.Now).WithMessage("To Date must be before today")
            .Must((model, ToDate) =>
            {
                return ToDate > model.FromDate;
            }).WithMessage("To day must be after From Date");

        RuleFor(x => x.PriceDiscount)
            .Cascade(CascadeMode.Stop)
            .Must((model, PriceDiscount) =>
            {
                return (model.DiscountType == Domain.Constant.DiscountType.PRICE && PriceDiscount > 0) || model.DiscountType == Domain.Constant.DiscountType.PERCENTAGE;
            })
            .WithMessage("If price discount type, must enter price");

        RuleFor(x => x.PercentageDiscount)
            .Cascade(CascadeMode.Stop)
            .Must((model, PercentageDiscount) =>
            {
                return (model.DiscountType == Domain.Constant.DiscountType.PERCENTAGE && PercentageDiscount > 0) || model.DiscountType == Domain.Constant.DiscountType.PRICE;
            })
            .WithMessage("If percentage discount type, must enter percentage");
    }
}
