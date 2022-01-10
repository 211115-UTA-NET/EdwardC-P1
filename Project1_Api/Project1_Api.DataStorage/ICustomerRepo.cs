using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Api.DataStorage
{
    public interface ICustomerRepo
    {
        Task<bool> GetCustomerByName(string Name);

        Task PostCustomer(List<string> customerInfo);
    }
}
