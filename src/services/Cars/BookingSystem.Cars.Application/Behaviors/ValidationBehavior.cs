using FluentValidation;
using MediatR;

namespace BookingSystem.Cars.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(request, cancellationToken)));
            var failures = validationResults.SelectMany(er => er.Errors).Where(er => er != null);

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }
        }

        return await next();
    }
}
