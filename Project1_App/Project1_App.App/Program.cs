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

            // Call Class 
            Login myLogin = new();
            NewCustomer newCustomer = new();
            CustomerRequest customerRequest = new();
            ManagerRequest managerRequest = new();
            Logout myLogout = new();

            // Http Request
            Customer customer = new(myServer);
            StoreLocation storeLocation = new(myServer);
            StoreInventory storeInventory = new(myServer);
            Invoice invoice = new(myServer);
            ItemDetailsInfo itemDetailsInfo = new(myServer);

            //Set for getStringInfo
            StoreLocation.SetUp();
            StoreInventory.SetUp();
            Invoice.SetUp();
            ItemDetailsInfo.SetUp();

            while (programRun)
            {

                switch (program.myMode)
                {
                    case Mode.Login:
                        program.CustomerId = 0;
                        program.StoreId = 0;
                        Console.WriteLine("Welcome to Login");
                        Console.WriteLine("----------------");
                        await myLogin.LoginScreen(program, customer);
                        break;
                    case Mode.NewCustomer:
                        Console.WriteLine("\nNew Customer's Sign up");
                        Console.WriteLine("----------------------");
                        newCustomer.test();
                        programRun = false;
                        break;
                    case Mode.CustomerRequest:
                        Console.WriteLine("\nCustomer Request");
                        Console.WriteLine("----------------");
                        await customerRequest.EnterCustomerScreen(program, storeLocation, invoice, itemDetailsInfo);
                        break;
                    case Mode.ManagerRequest:
                        Console.WriteLine("\nManager Request");
                        Console.WriteLine("---------------");
                        await managerRequest.EnterManagerScreen(program, customer, storeLocation, storeInventory, invoice);
                        break;
                    case Mode.SetOrder:
                        Console.WriteLine("\nSet Order");
                        Console.WriteLine("---------");
                        programRun = false;
                        break;
                    case Mode.Logout:
                        Console.WriteLine("\nLog out");
                        Console.WriteLine("-------");
                        myLogout.EnterLogoutScreen(program);
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