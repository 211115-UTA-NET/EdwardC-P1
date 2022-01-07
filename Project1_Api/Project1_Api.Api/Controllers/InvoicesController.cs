using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1_Api.DataStorage;

namespace Project1_Api.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceRepo _invoiceRepo;
        public InvoicesController(IInvoiceRepo invoiceRepo)
        {
            _invoiceRepo = invoiceRepo;
        }

        [HttpGet]
        public async Task<List<string>> GetAll()
        {
            List<string> getInvoice = await _invoiceRepo.GetAllInvoice();

            return getInvoice;
        }

        [HttpGet("/api/Invoices/StoreId")]
        public async Task<List<string>> GetInvoiceByStore([FromQuery] string storeId)
        {
            List<string> getInvoice = await _invoiceRepo.GetInvoiceByStoreId(storeId);

            return getInvoice;
        }

        [HttpGet("/api/Invoices/CustomerId")]
        public async Task<List<string>> GetInvoiceByCustomer([FromQuery] string customerId)
        {
            List<string> getInvoice = await _invoiceRepo.GetInvoiceByCustomerId(customerId);

            return getInvoice;
        }
    }
}
