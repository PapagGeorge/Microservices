using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace Infrastructure.Repos
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerServiceDbContext _context;

        public CustomerRepository(CustomerServiceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync(int pageNumber, int pageSize)
        {
            return await _context.Set<Customer>()
                .Include(c => c.Address)
                .OrderBy(c => c.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            return await _context.Set<Customer>()
                .Include(c => c.Address)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Set<Customer>().AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Set<Customer>().Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            var customer = await GetCustomerByIdAsync(id);
            if (customer != null)
            {
                _context.Set<Customer>().Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Address> GetAddressByIdAsync(Guid id)
        {
            return await _context.Set<Address>().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAddressAsync(Address address)
        {
            await _context.Set<Address>().AddAsync(address);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAddressAsync(Address address)
        {
            _context.Set<Address>().Update(address);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(Guid id)
        {
            var address = await GetAddressByIdAsync(id);
            if (address != null)
            {
                _context.Set<Address>().Remove(address);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Address>> GetAddressesByCustomerIdAsync(Guid customerId)
        {
            return await _context.Set<Address>()
                .Where(a => a.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
