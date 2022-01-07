using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_App.App
{
    public class Logout
    {
        public void EnterLogoutScreen(Program myProgram) // For Customer only
        {
            string? input;
            // prompt user to sign out or continue order other item
            Console.WriteLine("Want to log out or continure shopping?\nEnter '1' to Log out or '2' to continue order");
            input = Console.ReadLine();
            if (input == "1")
                myProgram.myMode = Program.Mode.Login;
            else if (input == "2")
                myProgram.myMode = Program.Mode.CustomerRequest;

            Console.WriteLine();
        }
    }
}
