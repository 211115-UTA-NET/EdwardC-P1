using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Api.DataStorage
{
    public interface IStoreInventoryRepo
    {
        Task<List<string>> GetAllStoreInventory();
        Task<List<string>> GetStoreInventoryById(string id);
    }
}
