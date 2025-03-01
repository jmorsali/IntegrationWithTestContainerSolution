﻿using API.Contracts.Requests;
using FluentValidation;
using System.Text.RegularExpressions;

namespace API.Validation;

public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
{
    public CustomerRequestValidator()
    {
        RuleFor(x => x.FullName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.GitHubUsername).NotEmpty();
        RuleFor(x => x.DateOfBirth).NotEmpty();

        RuleFor(x => x.GitHubUsername).Custom(ValidateGitHubUsername);
        RuleFor(x => x.Email).Custom(ValidateEmail);
        RuleFor(x => x.FullName).Custom(ValidateFullName);
        RuleFor(x => x.DateOfBirth).Custom(ValidateDateOfBirth);
    }

    private void ValidateFullName(string fullname, ValidationContext<CustomerRequest> context)
    {
        Regex fullNameRegex = new("^[a-z ,.'-]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        if (!fullNameRegex.IsMatch(fullname))
        {
            var message = $"{fullname} is not a valid full name";
            context.AddFailure(message);
        }
    }
    private void ValidateEmail(string email, ValidationContext<CustomerRequest> context)
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

    private void ValidateDateOfBirth(DateTime dateOfBirth, ValidationContext<CustomerRequest> context)
    {
        if (dateOfBirth > DateTime.Now.Date)
        {
            const string message = "Your date of birth cannot be in the future";
            context.AddFailure(message);
        }
    }
    private void ValidateGitHubUsername(string username, ValidationContext<CustomerRequest> context)
    {
        Regex usernameRegex = new("^[a-z\\d](?:[a-z\\d]|-(?=[a-z\\d])){0,38}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        if (!usernameRegex.IsMatch(username))
        {
            var message = $"{username} is not a valid username";
            context.AddFailure(message);
        }

    }
}