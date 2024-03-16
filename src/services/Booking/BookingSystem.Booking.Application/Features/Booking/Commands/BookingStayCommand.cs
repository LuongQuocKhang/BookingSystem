using BookingSystem.Booking.Domain.Constant;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Booking.Application.Features.Booking.Commands;

public class BookingStayCommand : IRequest<int>
{
    #region Booking information
    [Required]
    public int StayId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public int NumberOfAdults { get; set; }

    public int NumberOfChildren { get; set; }

    public int NumberOfInfants { get; set; }

    public string? MessagerForAuthor { get; set; } = string.Empty;
    #endregion

    #region Payment
    public PaymentMethod PaymentMethod { get; set; }

    public string CardNumber { get; set; } = string.Empty;

    public string CardHolder { get; set; } = string.Empty;

    public DateTime ExpirationDate { get; set; }

    public string? CVC { get; set; } = string.Empty;
    #endregion

    public int UserId { get; set; }
}
