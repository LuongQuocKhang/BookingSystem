namespace BookingSystem.Booking.Domain.Constant;

public enum Constant
{
    CASH = 1,
    PAYPAL = 2,
    CREDITCARD = 3,
    MOMO = 4,
    VNPAY = 5
}

public enum BookingStatus
{
    INIT = 1,
    PROCESSING = 2,
    CONFIRMED = 3,
    PAID = 4,
    COMPLETE = 5
}