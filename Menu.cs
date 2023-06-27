using ApplicationNo1.Users;
using ApplicationNo1.Users.Vehicles;
using System.Linq.Expressions;

namespace ApplicationNo1
{
    public class Menu
    {
        #region Fields

        private List<IUser> _usersList;
        private IUser? _iuser;
        private MenuItem? _currentMenu;
        private MenuItem?  _mainMenu;
        #endregion
       
        #region Constructor
        public Menu()
        {
            _usersList = new List<IUser>();
        }
        #endregion

        #region Methods
        public void Start()
        {
            //Initialize Menu
            InitializeMenu();

            //Make a choice
            MenuOptions();

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

        //USER COMMANDS
        public void CreateNewUser()
        {
            //Set Id
            var id = Guid.NewGuid().ToString();

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

            //First Trip
            var trip = StepCreation(country,0);

            //User's Creation Time
            var creationTime = DateTime.UtcNow;

            //User Creation
            var user = new User(country)
            {
                Id = id,
                Name = name,
                Age = age,
                CurrentCountry = country,
                Vehicle = vehicle,
                Wallet = wallet,
                Trip = trip,
                CreationTime = creationTime,
            };

            _iuser = user;
            //Adds user to user list
            _usersList.Add(_iuser);


            InsertWriteLine("User creation was succesfull.");

            //Adds user first trip to list
            _iuser.NewTrip(_iuser.CurrentCountry, 0);

            //BACK TO MENU - update current menu
            MenuGoBackOneStep();

            MenuOptions();
        }
        public void SelectUser()
        {
            if (CheckUsersAmount())
            {
                PrintsUserList();

                InsertWriteLine("To choose write the corresponding username.");

                var input = (string)GetUserInput(InputValidationTypes.None);

                var userSelected = _usersList.FirstOrDefault(x => x.Name == input);

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
                _iuser = userSelected;

                //SUBMENU
                MenuOptions();
            }
            else
            {
                MenuGoBackOneStep();
                MenuOptions();
            }
             
        }
        
        //SELECT USER SUBMENU
        public void ExecuteDrive()
        {
            InsertWriteLine("Give the distance you want to drive.");
            var input = (double)GetUserInput(InputValidationTypes.Double);

            var checkDrive = _iuser.Drive(input);

            if (checkDrive)
            {
                InsertWriteLine("What is your final destination? Choose: ");
                _iuser.CurrentCountry = CountrySelection();
                InsertWriteLine($"You drove {input} km. You now are in: {_iuser.CurrentCountry.Name}.");

                //Add to list
                _iuser.NewTrip(_iuser.CurrentCountry, input);

                //Increase total distance
                _iuser.Trip.TotalDistance += input;
                InsertWriteLine($"Total Distance driven -> {_iuser.Trip.TotalDistance:0.##}. Starting point -> {_iuser.StartingCountry.Name}");

                PrintStepsList();
            }
            else
                InsertWriteLine("There is not enough fuel to drive this distance.");

            //Back to options of Select User
            MenuGoBackOneStep();
            MenuOptions();
        }
        public void ExecuteRefuel()
        {
            InsertWriteLine($"Give cash to refuel. You have {_iuser.Wallet.Balance:0.##} {_iuser.CurrentCountry.Currency}.", true);
            var doubleInput = (double)GetUserInput(InputValidationTypes.Double);

            //Checks for money inside wallet
            var walletCheck = _iuser.Wallet.ChecksMoneyAvailable(doubleInput);

            if (walletCheck)
            {
                
                //Chris - GasPrice is already a property of User so no need to pass it as a param
                var results = _iuser.Refuel(doubleInput);

                switch (results)
                {
                    case VehicleBase.RefuelResults.NoChange:
                        //Payment
                        _iuser.Wallet.Payment(doubleInput);


                        InsertWriteLine($"Payment was succesfull. You now have: {_iuser.Wallet.Balance:0.##} {_iuser.CurrentCountry.Currency}.");
                        InsertWriteLine($"You filled your {_iuser.Vehicle.Name} with {_iuser.Vehicle.RefuelAmount:0.##} L and the new level of fuel is: {_iuser.Vehicle.FuelLevel:0.##} L.");
                        break;
                    case VehicleBase.RefuelResults.TooMuchMoneyNeedsChange:
                        //Payment
                        var moneyRequired = _iuser.Vehicle.RefuelAmount * _iuser.CurrentCountry.GasPrice;
                        _iuser.Wallet.Payment(moneyRequired);
                        var change = doubleInput - moneyRequired;


                        InsertWriteLine($"You paid {moneyRequired:0.##} and your change is {change:0.##} {_iuser.CurrentCountry.Currency}. You now have: {_iuser.Wallet.Balance:0.##} {_iuser.CurrentCountry.Currency}.");
                        InsertWriteLine($"You filled your {_iuser.Vehicle.Name} with {_iuser.Vehicle.RefuelAmount:0.##} L and the new level of fuel is: {_iuser.Vehicle.FuelLevel:0.##} L.");
                        break;
                }
            }
            else
            {
                InsertWriteLine("You don't have enough cash.");
            }
            //Back to options of Select User
            MenuGoBackOneStep();
            MenuOptions();
        }
        public void UserInfo()
        {
            InsertWriteLine($"User :{_iuser.Name} with ID {_iuser.Id} has {_iuser.Wallet.Balance} {_iuser.CurrentCountry.Currency}" +
                $" and drove {_iuser.Vehicle.KmCounter} km in {_iuser.CurrentCountry.Name}.");

            //Back to options of Select User
            MenuGoBackOneStep();
            MenuOptions();
        }
        public void SelectNewUser()
        {
            if (CheckUsersAmount())
            {
                //Back to options of Select User
                MenuGoBackOneStep();
                MenuGoBackOneStep();
                SelectUser();
            }
            else
                MenuGoBackOneStep();

        }
        //END OF SUBMENU

        public void ShowUsers()
        {
            CheckUsersAmount();
            InsertWriteLine(_currentMenu.Name);

            PrintsUserList();

            //BACK TO MENU - update current menu
            _currentMenu = _mainMenu;
            MenuOptions();
        }
        public void CloseApp()
        {
            InsertWriteLine($"--- {_currentMenu.Name} ---");
            Environment.Exit(0);
        }

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
                SubItems = menu
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
            var vehicle = vehiclesList.FirstOrDefault(x => x.SearchTerm == input);

            if (vehicle != null)
            {
                InsertWriteLine($"\nUser's vehicle -> {vehicle.Name}.\n");
            }
            else
            {
                InsertWriteLine("Try again. Give vehicle's first letter.\n");
                VehicleSelection();
            }

            return vehicle;
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
        private Wallet BalanceSelection()
        {
            InsertWriteLine("Give wallet balance:");
            var money = new Wallet();
            money.Balance = (double)GetUserInput(InputValidationTypes.Double);

            return money;
        }

        private ITrip StepCreation(Country country, double distance)
        {
            ITrip step = new Trip()
            {
                CountryLanded = country,
                DistanceTraveled = distance,
            };

            return step;
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
            if (_usersList.Count == 0)
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
            foreach (var user in _usersList)
            {
                var index = _usersList.IndexOf(user) + 1;
                InsertWriteLine($"{index}) Name:{user.Name},ID {user.Id}, Age:{user.Age}, Starting destination: {user.CurrentCountry.Name}, " +
                    $"Vehicle: {user.Vehicle.Name}, Money Balance: {user.Wallet.Balance} {user.CurrentCountry.Currency}, Created:{user.CreationTime.ToString("h:mm:ss tt")}");
            }
        }

        private void PrintStepsList()
        {
            InsertWriteLine("---TRIP STEPS---");
            foreach (var step in _iuser.Trip.Steps)
            {
                var index = _iuser.Trip.Steps.IndexOf(step);
                InsertWriteLine($"{index}) Distance traveled -> {step.DistanceTraveled} km. Final destination -> {step.CountryLanded.Name}.");
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
    
