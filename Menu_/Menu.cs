using ApplicationNo1.Country_;
using ApplicationNo1.Vehicle_;
using ApplicationNo1.Wallet_;
using ApplicationNo1.User_;
using ApplicationNo1.Trip_;
using System.Reflection;
using Microsoft.Extensions.Hosting;

namespace ApplicationNo1.Menu_
{
    public class Menu
    {
        #region Fields
        private IUser? _icurrentUser;
        private IUserService _userService;
        private ITripService _tripService;
        private MenuItem? _currentMenu;
        private MenuItem? _mainMenu;
        #endregion

        #region Constructor
        public Menu(IUserService userService, ITripService tripService)
        {
            _userService = userService;
            _tripService = tripService;
        }
        #endregion

        #region Methods
        public void Start()
        {
            //Initialize Menu
            InitializeMenu();

            //Make a choice
            _currentMenu.UserSelectionAction();

        }
        public void MenuOptions()
        {
            //Get Current Menu
            InsertWriteLine("--- " + _currentMenu.Name + " ---");
            var options = _currentMenu.SubItems;

            //Show Options
            foreach (var option in options)
            {
                InsertWriteLine($"[{option.UserSelection}]  {option.Name}");
            }

            InsertWriteLine("\nMake a choice:\n");

            var input = (int)GetUserInput(InputValidationTypes.Int);

            var choice = options.FirstOrDefault(x => x.UserSelection == input);

            if (choice == null)
            {
                InsertWriteLine("Please choose from the available options.\n");
                MenuOptions();
            }

            //Update current menu
            _currentMenu = choice;
            InsertWriteLine($"--- {_currentMenu.Name} ---", true);
            choice.UserSelectionAction();
        }

        #region Menu Commands
        public void MainMenuAction()
        {
            #region Clear Stuff in Memory
            _icurrentUser = null;
            #endregion

            MenuOptions();
        }

        [MenuItemAction("CreateNewUser")]
        public void CreateNewUser()
        {
            //Name Input
            InsertWriteLine("Give name: ");
            var name = (string)GetUserInput(InputValidationTypes.None);

            //Age Input
            InsertWriteLine("Give age: ");
            var age = (int)GetUserInput(InputValidationTypes.Int);

            //Starting Country 
            var country = CountrySelection();

            //Vehicle Input
            var vehicle = VehicleSelection();

            //Wallet Balance Input
            var wallet = BalanceSelection();

            //User's Creation Time
            var creationTime = DateTime.UtcNow;

            //Adds user to user list
            _userService.AddNewUser(new User(country, _tripService)
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Age = age,
                Vehicle = vehicle,
                Wallet = wallet,
                CreationTime = creationTime,
            });

            InsertWriteLine("User creation was succesfull.");

            //BACK TO MENU - update current menu
            MenuGoBackOneStep();
        }

        [MenuItemAction("SelectUser")]
        public void SelectUser()
        {
            if (CheckUsersAmount())
            {
                if (_icurrentUser == null)
                {
                    PrintsUserList();

                    InsertWriteLine("To choose write the corresponding username.");

                    var input = (string)GetUserInput(InputValidationTypes.None);

                    var userSelected = _userService.Users.FirstOrDefault(x => x.Name == input);

                    if (userSelected != null)
                    {
                        InsertWriteLine($"You selected user {userSelected.Name}");
                    }
                    else
                    {
                        InsertWriteLine("Please write the username of your choice.\n");
                        SelectUser();
                    }

                    //Update chosen user
                    _icurrentUser = userSelected;
                }
                //SUBMENU
                MenuOptions();
            }
            else
                MenuGoBackOneStep();
        }

        #region SelectUser Inside Commands
        public void ExecuteDrive()
        {
            InsertWriteLine("Give the distance you want to drive.");
            var input = (double)GetUserInput(InputValidationTypes.Double);

            InsertWriteLine("What is your final destination? Choose: ");

            var checkDrive = _icurrentUser.Drive(input, CountrySelection());

            if (checkDrive)
            {
                InsertWriteLine($"You drove {input} km from {_icurrentUser.StartingCountry.Name}.");
                PrintTripsList();
            }
            else
                InsertWriteLine("There is not enough fuel to drive this distance.");

            //Back to options of Select User
            MenuGoBackOneStep();
        }
        public void ExecuteRefuel()
        {
            InsertWriteLine($"Give cash to refuel. You have {_icurrentUser.Wallet.Balance:0.##} {_icurrentUser.CurrentCountry.Currency}.", true);
            var doubleInput = (double)GetUserInput(InputValidationTypes.Double);

            //Checks for money inside wallet
            var walletCheck = _icurrentUser.CheckBalance(doubleInput);

            if (walletCheck)
            {
                //Run Refuel and Pay
                var results = _icurrentUser.Refuel(doubleInput);
                _icurrentUser.PaymentForFuel();

                //Inform Application User
                switch (results)
                {
                    case VehicleBase.RefuelResults.FuelOrderSatisfied:

                        InsertWriteLine($"Payment was succesfull. You now have: {_icurrentUser.Wallet.Balance:0.##} {_icurrentUser.CurrentCountry.Currency}.");
                        InsertWriteLine($"You filled your {_icurrentUser.Vehicle.Name} with {_icurrentUser.Vehicle.RefuelAmount:0.##} L and the new level of fuel is: {_icurrentUser.Vehicle.FuelLevel:0.##} L.");
                        break;
                    case VehicleBase.RefuelResults.FuelOrderNotSatisfied:
                        InsertWriteLine($"Payment was succesfull. You now have: {_icurrentUser.Wallet.Balance:0.##} {_icurrentUser.CurrentCountry.Currency}.");
                        InsertWriteLine($"You filled your {_icurrentUser.Vehicle.Name} with {_icurrentUser.Vehicle.RefuelAmount:0.##} L and the new level of fuel is: {_icurrentUser.Vehicle.FuelLevel:0.##} L.");
                        break;
                }
            }
            else
            {
                InsertWriteLine("You don't have enough cash.");
            }
            //Back to options of Select User
            MenuGoBackOneStep();
        }
        public void UserInfo()
        {
            InsertWriteLine($"User :{_icurrentUser.Name} with ID {_icurrentUser.Id} has {_icurrentUser.Wallet.Balance:0.##} {_icurrentUser.CurrentCountry.Currency}" +
                $" and drove {_icurrentUser.Vehicle.KmCounter} km until {_icurrentUser.CurrentCountry.Name}.");

            //Back to options of Select User
            MenuGoBackOneStep();
        }
        public void SelectNewUser()
        {
            _icurrentUser = null;
            MenuGoBackOneStep();
        }

        #endregion

        [MenuItemAction("ShowUsers")]
        public void ShowUsers()
        {
            if (CheckUsersAmount())
            {
                PrintsUserList();
            }

            //BACK TO MENU - update current menu
            MenuGoBackOneStep();
        }

        [MenuItemAction("RandomTest")]
        public void RandomTest()
        {
            var type = typeof(Menu);
            MethodInfo[] methods = type.GetMethods();

            foreach (var method in methods)
            {
                // Check if the method has the custom attribute
                var attribute = method.GetCustomAttribute<MenuItemActionAttribute>();
                if (attribute != null)
                {
                    InsertWriteLine(attribute.MethodName);
                }
            }

            //BACK TO MENU - update current menu
            MenuGoBackOneStep();
        }
        [MenuItemAction("CloseApp")]
        public void CloseApp()
        {
            Environment.Exit(0);
        }
        #endregion

        #endregion

        #region Initializations
        private void InitializeMenu()
        {
            //Select user menu
            var selectUserMenu = new List<MenuItem>()
            {
                new MenuItem()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Drive",
                    UserSelection = 1,
                    UserSelectionAction = ExecuteDrive
                },
                new MenuItem()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Refuel",
                    UserSelection = 2,
                    UserSelectionAction  = ExecuteRefuel
                },
                new MenuItem()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Information",
                    UserSelection = 3,
                    UserSelectionAction = UserInfo
                },
                new MenuItem()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Select New User",
                    UserSelection = 4,
                    UserSelectionAction = SelectNewUser
                }
            };

            //Main menu
            var menu = new List<MenuItem>()
            {
                new MenuItem()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Create New User",
                    UserSelection = 1,
                    UserSelectionAction = CreateNewUser
                },
                 new MenuItem()
                {
                    Id=Guid.NewGuid().ToString(),
                    Name = "Select User",
                    UserSelection = 2,
                    SubItems = selectUserMenu,
                    UserSelectionAction = SelectUser
                },
                new MenuItem()
                {
                    Id=Guid.NewGuid().ToString(),
                    Name = "Show Users",
                    UserSelection = 3,
                    UserSelectionAction = ShowUsers
                },
                new MenuItem()
                {
                    Id=Guid.NewGuid().ToString(),
                    Name = "Random Test",
                    UserSelection = 4,
                    UserSelectionAction = RandomTest
                },
                new MenuItem()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "EXIT",
                    UserSelection = 0,
                    UserSelectionAction = CloseApp
                }
            };

            _mainMenu = new MenuItem()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Main Menu",
                UserSelection = 1,
                SubItems = menu,
                UserSelectionAction = MainMenuAction
            };

            _currentMenu = _mainMenu;
        }
        private IVehicle VehicleSelection()
        {
            List<IVehicle> vehiclesList = new List<IVehicle>()
            {
                new Car() { Name = "Car", SearchTerm = "C" },
                new Bus() { Name = "Bus", SearchTerm = "B" },
                new Moto() { Name = "Moto", SearchTerm = "M" }
            };

            InsertWriteLine("Select user's vehicle: ");
            foreach (var vehicleElement in vehiclesList)
            {
                var index = vehiclesList.IndexOf(vehicleElement) + 1;
                InsertWriteLine($"{index}) {vehicleElement.Name} -> {vehicleElement.SearchTerm}");
            }

            //Inputs
            var input = (string)GetUserInput(InputValidationTypes.None);
            var ivehicle = vehiclesList.FirstOrDefault(x => x.SearchTerm == input);

            if (ivehicle != null)
            {
                InsertWriteLine($"\nUser's vehicle -> {ivehicle.Name}.\n");
            }
            else
            {
                InsertWriteLine("Try again. Give vehicle's first letter.\n");
                VehicleSelection();
            }

            return ivehicle;
        }
        private Country CountrySelection()
        {
            List<Country> countriesList = new List<Country>()
            {
                new Country() { Name = "Bahamas" , Currency = "BSD" , GasPrice = 1.363, SearchTerm = "B" },
                new Country() { Name = "Greece"  , Currency = "EUR" , GasPrice = 1.865, SearchTerm = "G" },
                new Country() { Name = "Italy"   , Currency = "EUR" , GasPrice = 1.832, SearchTerm = "I" },
                new Country() { Name = "Japan"   , Currency = "JPY" , GasPrice = 168.1, SearchTerm = "J" },
                new Country() { Name = "Norway"  , Currency = "NOK" , GasPrice = 23.13, SearchTerm = "N" }
            };

            InsertWriteLine("Select country:");
            foreach (var countryElement in countriesList)
            {
                var index = countriesList.IndexOf(countryElement) + 1;
                InsertWriteLine($"{index}) {countryElement.Name} -> {countryElement.SearchTerm}");
            }
            //Inputs
            var input = (string)GetUserInput(InputValidationTypes.None);
            var country = countriesList.FirstOrDefault(x => x.SearchTerm == input);

            if (country != null)
            {
                InsertWriteLine($"Current user's country -> {country.Name}.\n");
            }
            else
            {
                InsertWriteLine("Try again. Give the country's first letter.\n");
                CountrySelection();
            }

            return country;

        }
        private IWallet BalanceSelection()
        {
            InsertWriteLine("Give wallet balance:");
            var money = (double)GetUserInput(InputValidationTypes.Double);
            IWallet ibalance = new Wallet(money);
            
            return ibalance;
        }

        #endregion

        #region Input & Validations
        private object GetUserInput(InputValidationTypes validationType)
        {
            var input = GenericInputChecks(Console.ReadLine());

            switch (validationType)
            {
                case InputValidationTypes.Double:
                    double numberDouble = 0;
                    var DoubleConversionSuccess = double.TryParse(input, out numberDouble) ? !(numberDouble < 0) : false;
                    if (DoubleConversionSuccess)
                        return numberDouble;
                    break;
                case InputValidationTypes.Int:
                    int numberInt = 0;
                    var IntConversionSuccess = int.TryParse(input, out numberInt) ? !(numberInt < 0) : false;
                    if (IntConversionSuccess)
                        return numberInt;
                    break;
                case InputValidationTypes.None:
                    return input;
            }

            InsertWriteLine("Try again:");
            return GetUserInput(validationType);
        }
        private string GenericInputChecks(string input)
        {
            if (input == "GO BACK")
            {
                if (!_currentMenu.Equals(_mainMenu))
                {
                    MenuGoBackOneStep();
                }
                MenuOptions();
            }
            else if (input == "MAIN MENU")
            {
                _currentMenu = _mainMenu;
                MenuOptions();
            }

            return input;
        }
        public bool CheckUsersAmount()
        {
            if (_userService.Users.Count == 0)
            {
                InsertWriteLine("Zero users in the system.\n");
                return false;
            }
            return true;
        }

        #endregion

        #region Utilities
        private void InsertWriteLine(string originalWriteLine, bool insertInputDefaults = false)
        {
            var test = insertInputDefaults ? " [MAIN MENU -> Type MAIN MENU] [PREVIOUS MENU -> Type GO BACK]\n" : "";
            Console.WriteLine(originalWriteLine + test);
        }
        private void MenuGoBackOneStep()
        {
            var parent = GetMenuItemParentByChildId(_mainMenu, _currentMenu.Id);
            _currentMenu = parent;

            if (_currentMenu.UserSelectionAction == null)
            {
                MenuOptions();
            }
            else
            {
                _currentMenu.UserSelectionAction();
            }
        }
        private MenuItem GetMenuItemParentByChildId(MenuItem menuItemToSearch, string id)
        {
            //Check here
            var itemWasFound = menuItemToSearch.SubItems.Any(x => x.Id == id);

            if (!itemWasFound)
            {
                foreach (var menuItem in menuItemToSearch.SubItems)
                {
                    var itemFound = GetMenuItemParentByChildId(menuItem, id);
                    if (itemFound != null)
                        return itemFound;
                }
            }
            else
            {
                return menuItemToSearch;
            }

            return null;
        }
        private void PrintsUserList()
        {
            InsertWriteLine("---USERS---");
            foreach (var user in _userService.Users)
            {
                var index = _userService.Users.IndexOf(user) + 1;
                InsertWriteLine($"{index}) Name:{user.Name},ID {user.Id}, Age:{user.Age}, Starting destination: {user.StartingCountry.Name}, " +
                    $"Vehicle: {user.Vehicle.Name}, Money Balance: {user.Wallet.Balance} {user.CurrentCountry.Currency}, Created:{user.CreationTime.ToString("h:mm:ss tt")}");
            }
        }
        private void PrintTripsList()
        {
            InsertWriteLine("---TRIPS OF ALL USERS---");
            foreach (var trip in _tripService.Trips)
            {
                var index = _tripService.Trips.IndexOf(trip) + 1;
                InsertWriteLine($"{index}) UserID: {trip.UserID}, Total distance driven: {trip.TotalDistance} km, Vehicle: {trip.UserVehicle.Name}.");
            }
        }
        #endregion

        #region Enums
        public enum InputValidationTypes
        {
            None,
            Double,
            Int
        }
        #endregion
    }
}


