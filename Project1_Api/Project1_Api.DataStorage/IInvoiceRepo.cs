using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Api.DataStorage
{
    public interface IInvoiceRepo
    {
        Task<List<string>> GetAllInvoice();
        Task<List<string>> GetInvoiceByStoreId(string num);
        Task<List<string>> GetInvoiceByCustomerId(string num);
    }
}
