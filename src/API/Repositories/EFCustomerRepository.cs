using API.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class EFCustomerRepository : ICustomerRepository
{
    private readonly SaleDbStore _context;

    public EFCustomerRepository(SaleDbStore context)
    {
        _context = context;
    }
    public async Task<bool> CreateAsync(Customer customer)
    {
       _context.Add(customer);
        return (await _context.SaveChangesAsync()) > 0;
    }

    public async Task<Customer> GetAsync(Guid id)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        try
        {
            _context.Update(customer);
            return await _context.SaveChangesAsync() > 0;
        }
        catch(Exception ex) 
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
        if (customer != null)
        {
            _context.Remove(customer);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }
}