using Project1_App.App.RequestHttp;

namespace Project1_App.App
{
    public class Program
    {
        public static readonly HttpClient httpClient = new();
        protected internal Mode myMode { get; set; }
        protected internal int CustomerId { get; set; }
        protected internal int StoreId { get; set; }

        public enum Mode { Login, NewCustomer, CustomerRequest, ManagerRequest, SetOrder, Logout, Exit }
        // This code line help me to manage individual class file

        public async static Task Main(string[] args)
        {
            bool programRun = true;

            Program program = new();
            program.myMode = Mode.Login;
            //Uri myServer = new(await File.ReadAllTextAsync("C:/Users/rootb/Revature/Database_File/LocalHost2.txt"));
            //Uri myServer = new("https://211115bikeshop.azurewebsites.net");
            Uri myServer = new(await File.ReadAllTextAsync("C:/Users/rootb/Revature/Database_File/LocalHostP1.txt"));

            //Set for getStringInfo
            GetStringInfo.getStringInfo(myServer);
            ModifyInformation.getStringInfo(myServer);

            while (programRun)
            {

                switch (program.myMode)
                {
                    case Mode.Login:
                        program.CustomerId = 0;
                        program.StoreId = 0;
                        Console.WriteLine("Welcome to Login");
                        Console.WriteLine("----------------");
                        await Login.LoginScreen(program);
                        break;
                    case Mode.NewCustomer:
                        Console.WriteLine("\nNew Customer's Sign up");
                        Console.WriteLine("----------------------");
                        await NewCustomer.EnterNewCustomerScreen(program);
                        break;
                    case Mode.CustomerRequest:
                        Console.WriteLine("\nCustomer Request");
                        Console.WriteLine("----------------");
                        await CustomerRequest.EnterCustomerScreen(program);
                        break;
                    case Mode.ManagerRequest:
                        Console.WriteLine("\nManager Request");
                        Console.WriteLine("---------------");
                        await ManagerRequest.EnterManagerScreen(program);
                        break;
                    case Mode.SetOrder:
                        Console.WriteLine("\nSet Order");
                        Console.WriteLine("---------");
                        programRun = false;
                        break;
                    case Mode.Logout:
                        Console.WriteLine("\nLog out");
                        Console.WriteLine("-------");
                        Logout.EnterLogoutScreen(program);
                        programRun = false;
                        break;
                    case Mode.Exit:
                        Console.WriteLine("\nExit");
                        programRun = false;
                        break;
                }
            }

        }
    }
}