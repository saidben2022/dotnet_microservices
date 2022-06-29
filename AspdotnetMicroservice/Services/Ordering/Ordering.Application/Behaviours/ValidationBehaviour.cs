using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ValidationExeption = Ordering.Application.Exceptions.ValidationException;//added so that we use our own ValidationException class and not the default exeption class
namespace Ordering.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : MediatR.IRequest<TResponse>// you should add these line or it will not work 
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures=validationResults.SelectMany(r=>r.Errors).Where(f=>f!=null).ToList();
                if(failures.Count()!=0)
                {
                    throw new ValidationExeption(failures);
                }
              

            }
            return await next();
        }
    }
}
