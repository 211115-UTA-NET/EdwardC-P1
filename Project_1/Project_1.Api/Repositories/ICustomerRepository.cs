using Project_1.Api.Models;

namespace Project_1.Api.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> Get(int id);
        Task<Customer> Post(Customer customer);
        Task Put(Customer customer);
        Task Delete(int id);
    }
}
