using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1_Api.DataStorage;

namespace Project1_Api.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemDetailsController : ControllerBase
    {
        private readonly IItemDetailsRepo _itemDetailRepo;
        public ItemDetailsController(IItemDetailsRepo itemDetails)
        {
            _itemDetailRepo = itemDetails;
        }

        [HttpGet]
        public async Task<List<string>> GetItemDetails()
        {
            List<string> result = await _itemDetailRepo.GetItems();

            return result;
        }
    }
}
