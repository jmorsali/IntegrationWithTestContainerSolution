using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace API.Domain.Common;

public class DateOfBirth : ValueOf<DateTime, DateOfBirth>
{
    protected override void Validate()
    {
        if (Value > DateTime.Now.Date)
        {
            const string message = "Your date of birth cannot be in the future";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(DateOfBirth), message)
            });
        }
    }
}
