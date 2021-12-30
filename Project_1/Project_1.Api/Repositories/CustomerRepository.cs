#nullable disable
using Project_1.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Project_1.Api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;

        public CustomerRepository(CustomerContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }
        
        public async Task<Customer> Get(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> Post(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task Put(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        
        public async Task Delete(int id)
        {
            var customerToDelete = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customerToDelete!);
            await _context.SaveChangesAsync();
        }
    }
}
