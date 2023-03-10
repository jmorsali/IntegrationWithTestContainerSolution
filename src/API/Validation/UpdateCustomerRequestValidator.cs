using System.Text.RegularExpressions;
using API.Contracts.Requests;
using FluentValidation;

namespace API.Validation;

public class UpdateCustomerRequestValidator :  AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerRequestValidator()
    {
        RuleFor(x => x.Customer.FullName).NotEmpty();
        RuleFor(x => x.Customer.Email).NotEmpty();
        RuleFor(x => x.Customer.GitHubUsername).NotEmpty();
        RuleFor(x => x.Customer.DateOfBirth).NotEmpty();

        RuleFor(x => x.Customer.GitHubUsername).Custom(ValidateGitHubUsername);
        RuleFor(x => x.Customer.Email).Custom(ValidateEmail);
        RuleFor(x => x.Customer.FullName).Custom(ValidateFullName);
        RuleFor(x => x.Customer.DateOfBirth).Custom(ValidateDateOfBirth);
    }

    private void ValidateFullName(string fullname, ValidationContext<UpdateCustomerRequest> context)
    {
        Regex fullNameRegex = new("^[a-z ,.'-]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        if (!fullNameRegex.IsMatch(fullname))
        {
            var message = $"{fullname} is not a valid full name";
            context.AddFailure(message);
        }
    }
    private void ValidateEmail(string email, ValidationContext<UpdateCustomerRequest> context)
    {
        Regex emailRegex =
            new("^[\\w!#$%&’*+/=?`{|}~^-]+(?:\\.[\\w!#$%&’*+/=?`{|}~^-]+)*@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

        if (!emailRegex.IsMatch(email))
        {
            var message = $"{email} is not a valid email address";
            context.AddFailure(message);
        }

    }

    private void ValidateDateOfBirth(DateTime dateOfBirth, ValidationContext<UpdateCustomerRequest> context)
    {
        if (dateOfBirth > DateTime.Now.Date)
        {
            const string message = "Your date of birth cannot be in the future";
            context.AddFailure(message);
        }
    }
    private void ValidateGitHubUsername(string username, ValidationContext<UpdateCustomerRequest> context)
    {
        Regex usernameRegex = new("^[a-z\\d](?:[a-z\\d]|-(?=[a-z\\d])){0,38}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        if (!usernameRegex.IsMatch(username))
        {
            var message = $"{username} is not a valid username";
            context.AddFailure(message);
        }

    }
}