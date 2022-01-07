using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1_Api.DataStorage;

namespace Project1_Api.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreInventorysController : ControllerBase
    {
        private readonly IStoreInventoryRepo _storeInventoryRepo;

        public StoreInventorysController(IStoreInventoryRepo storeInventoryRepo)
        {
            _storeInventoryRepo = storeInventoryRepo;
        }

        [HttpGet]
        public async Task<List<string>> GetAll()
        {
            List<string> getInventory = await _storeInventoryRepo.GetAllStoreInventory();

            return getInventory;
        }

        [HttpGet("/api/StoreInventorys/Id")]
        public async Task<List<string>> GetInventoryById([FromQuery] string id)
        {
            List<string> getInventory = await _storeInventoryRepo.GetStoreInventoryById(id);

            return getInventory;
        }
    }
}
