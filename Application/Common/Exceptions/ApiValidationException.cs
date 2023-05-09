using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;

namespace Application.Common.Exceptions;

public class ApiValidationException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    public IDictionary<string, string[]> Errors { get; }

    public ApiValidationException()
        : base("Your request didn't pass the validation")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ApiValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        StatusCode = HttpStatusCode.UnprocessableEntity;

        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, Errors.Select( x => x.Value[0]));
    }

}

