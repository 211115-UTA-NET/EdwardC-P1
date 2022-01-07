using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1_Api.DataStorage;

namespace Project1_Api.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreLocationsController : ControllerBase
    {
        private readonly IStoreLocationRepo _storeLocationRepo;

        public StoreLocationsController(IStoreLocationRepo storeLocationRepo)
        {
            _storeLocationRepo = storeLocationRepo;
        }

        [HttpGet]
        public async Task<List<string>> GetAllLocations()
        {
            List<string> getLocation = await _storeLocationRepo.GetStoreLocation();

            return getLocation;
        }
    }
}
