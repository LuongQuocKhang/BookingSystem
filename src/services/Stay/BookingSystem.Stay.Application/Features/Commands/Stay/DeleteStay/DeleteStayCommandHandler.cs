using BookingSystem.Stay.Application.Contracts.Persistance;
using MediatR;

namespace BookingSystem.Stay.Application.Features.Commands.Stay.DeleteStay
{
    public class DeleteStayCommandHandler : IRequestHandler<DeleteStayCommand, bool>
    {
        private readonly IStayRepository _stayRepository;

        public DeleteStayCommandHandler(IStayRepository stayRepository)
        {
            _stayRepository = stayRepository;
        }

        public async Task<bool> Handle(DeleteStayCommand request, CancellationToken cancellationToken)
        {
            await _stayRepository.DeleteStay(request.StayId).ConfigureAwait(false);

            return true;
        }
    }
}
