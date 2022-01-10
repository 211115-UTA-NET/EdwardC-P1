using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1_Api.Api.Models;
using Project1_Api.DataStorage;

namespace Project1_Api.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepo _customerRepo;
        public CustomersController(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpGet("/api/Customers/Name")]
        public async Task<bool> GetInvoiceByStore([FromQuery] string Name)
        {
            bool result = await _customerRepo.GetCustomerByName(Name);

            return result;
        }

        [HttpPost("/api/Customers/Add")]
        public async Task AddNewCustomer([FromQuery] NewCustomer customer)
        {
            List<string> customerInfo = new() { customer.FirstName!, customer.LastName!, customer.Phone!,
                                                customer.Address!, customer.Username!, customer.Password! };
            await _customerRepo.PostCustomer(customerInfo, false);
        }
    }
}
