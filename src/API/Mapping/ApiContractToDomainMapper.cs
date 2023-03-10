using API.Contracts.Requests;
using API.Domain;

namespace API.Mapping;

public static class ApiContractToDomainMapper
{
    public static Customer ToCustomer(this CustomerRequest request)
    {
        return new Customer
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            GitHubUsername = request.GitHubUsername,
            FullName = request.FullName,
            DateOfBirth = request.DateOfBirth.Date
        };
    }

    public static Customer ToCustomer(this UpdateCustomerRequest request)
    {
        return new Customer
        {
            Id =request.Id,
            Email =request.Customer.Email,
            GitHubUsername = request.Customer.GitHubUsername,
            FullName = request.Customer.FullName,
            DateOfBirth = request.Customer.DateOfBirth.Date
        };
    }
}
