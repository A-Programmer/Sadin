
using FluentValidation.Results;

namespace Sadin.Common.CustomExceptions;

public class KsValidationException: Exception
{
    public IEnumerable<ValidationFailure> Errors { get; }

    public KsValidationException(IEnumerable<ValidationFailure> errors) => Errors = errors;
    
    public KsValidationException(IEnumerable<ValidationFailure> errors,string message)
        : base(message) => Errors = errors;
}