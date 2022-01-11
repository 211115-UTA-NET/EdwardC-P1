using Project1_App.App.RequestHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_App.App
{
    public static class SetOrder
    {
        public static async Task EnterSetOrder(Program program)
        {
            bool IsApproved = false;

            Console.Write("Your Store: " + await StoreLocation.DisplayStoreLocationById(program.StoreId));


            await ProcessOrder.EnterProcessOrder(program);
        }

        public static async Task StartOrder()
        {
            bool OrderMore = true;
            do
            {
                Console.WriteLine("Chose a item you want by number: ");

            }
            while(OrderMore);
        }
    }
}
