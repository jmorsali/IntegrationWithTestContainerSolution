using API.Contracts.Data;
using API.Domain;

namespace API.Mapping;

public static class DomainToDtoMapper
{
    public static CustomerDto? ToCustomerDto(this Customer? customer)
    {
        if (customer == null)
            return null;

        return new CustomerDto
        {
            Id = customer.Id,
            Email = customer.Email,
            GitHubUsername = customer.GitHubUsername,
            FullName = customer.FullName,
            DateOfBirth = customer.DateOfBirth.Date
        };
    }
}
