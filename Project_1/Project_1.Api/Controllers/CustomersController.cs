using Project_1.Api.Models;
using Project_1.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project_1.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _customerRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customerToGet = await _customerRepository.Get(id);
            if(customerToGet == null)
            {
                return NotFound();
            }

            return await _customerRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer([FromBody] Customer customer)
        {
            var newCustomer = await _customerRepository.Post(customer);
            return CreatedAtAction(nameof(GetCustomers), new { id = newCustomer.CustomerId }, newCustomer);
        }

        [HttpPut]
        public async Task<ActionResult<Customer>> PutCustomer(int id, [FromBody] Customer customer)
        {
            if(id != customer.CustomerId)
            {
                return BadRequest();
            }

            await _customerRepository.Put(customer);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var customerToDelete = await _customerRepository.Get(id);
            if (customerToDelete == null)
                return NotFound();

            await _customerRepository.Delete(customerToDelete.CustomerId);
            return NoContent();
        }
    }
}
