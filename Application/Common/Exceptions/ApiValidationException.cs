using FluentValidation.Results;

namespace Application.Common.Exceptions;

public class ApiValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ApiValidationException()
        : base("Your request didn't pass the validation")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ApiValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

}

