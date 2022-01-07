using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Api.DataStorage
{
    public interface ILoginRepo
    {
        Task<List<string>> checkUsernamePassword(string username, string password);
    }
}
