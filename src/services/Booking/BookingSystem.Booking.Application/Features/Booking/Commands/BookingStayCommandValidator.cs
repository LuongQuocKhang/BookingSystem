using BookingSystem.Booking.Domain.Constant;
using FluentValidation;

namespace BookingSystem.Booking.Application.Features.Booking.Commands;

public class BookingStayCommandValidator : AbstractValidator<BookingStayCommand>
{
    public BookingStayCommandValidator()
    {
        RuleFor(x => x.StayId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Must enter stay Id.")
            .GreaterThan(0).WithMessage("Stay Id must be greater then zero.");

        RuleFor(x => x.NumberOfAdults)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Must enter Number Of Adults.")
            .GreaterThan(0).WithMessage("Number Of Adults must be greater then zero.");

        RuleFor(x => x.CheckInDate)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Must enter Check In date")
            .GreaterThan(DateTime.Now).WithMessage("Check In date must be after today")
            .Must((model, CheckInDate) =>
            {
                return CheckInDate < model.CheckOutDate;
            }).WithMessage("Check In date must be before Check Out date");

        RuleFor(x => x.CheckOutDate)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Must enter Check Out date")
            .GreaterThan(DateTime.Now).WithMessage("Check In date must be after today")
            .Must((model, CheckOutDate) =>
            {
                return CheckOutDate > model.CheckInDate;
            }).WithMessage("Check Out date must be after Check In date");


        RuleFor(x => x.PaymentMethod)
            .Cascade(CascadeMode.Stop)
            .Must((model, paymentMethod) =>
            {
                if (paymentMethod == PaymentMethod.CreditCard)
                {
                    if (string.IsNullOrEmpty(model.CardNumber)
                        || string.IsNullOrEmpty(model.CardHolder)
                        || string.IsNullOrEmpty(model.CVC))
                    {
                        return false;
                    }

                    if (model.ExpirationDate < DateTime.Now)
                    {
                        return false;
                    }
                }

                return true;
            })
            .WithMessage("Card information not correct");
    }
}
