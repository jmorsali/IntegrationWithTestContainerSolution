using API.Contracts.Data;
using API.Domain;

namespace API.Repositories;

public interface ICustomerRepository
{
    Task<bool> CreateAsync(Customer customer);

    Task<Customer> GetAsync(Guid id);

    Task<IEnumerable<Customer>> GetAllAsync();

    Task<bool> UpdateAsync(Customer customer);

    Task<bool> DeleteAsync(Guid id);
}
