using FluentValidation.Results;
using System.Net;

namespace Application.Common.Exceptions;

public class ApiValidationException : Exception
{
    public HttpStatusCode HttpStatusCode { get; set; }
    public IDictionary<string, string[]> Errors { get; }

    public ApiValidationException()
        : base("Your request didn't pass the validation")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ApiValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        HttpStatusCode = HttpStatusCode.BadRequest;

        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

}

