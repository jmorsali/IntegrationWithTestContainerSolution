using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace API.Domain.Common;

public class FullName : ValueOf<string, FullName>
{
    
    protected override void Validate()
    {
       
    }
}
