using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
