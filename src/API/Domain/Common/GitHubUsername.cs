using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace API.Domain.Common;

public class GitHubUsername : ValueOf<string, GitHubUsername>
{
    

    protected override void Validate()
    {
        
    }
}
