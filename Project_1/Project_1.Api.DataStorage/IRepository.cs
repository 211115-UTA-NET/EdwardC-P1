
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1.Api.DataStorage
{
    public interface IRepository
    {
        Task<bool> CustomerExistAsync(string name);
    }
}
