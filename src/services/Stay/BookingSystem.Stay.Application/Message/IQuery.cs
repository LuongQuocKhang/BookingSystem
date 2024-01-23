using MediatR;
namespace BookingSystem.Stay.Application.Message;

public interface IQuery<TResposne> : IRequest<TResposne>
{
}
