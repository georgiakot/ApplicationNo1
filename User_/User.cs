using ApplicationNo1.Country_;
using ApplicationNo1.Trip_;
using ApplicationNo1.Vehicle_;
using ApplicationNo1.Wallet_;
using static ApplicationNo1.Vehicle_.VehicleBase;

namespace ApplicationNo1.User_
{
    public class User : IUser
    {
        #region Fields
        private Wallet? _wallet;
        private IVehicle? _vehicle;
        private Country? _currentCountry;
        private ITrip? _trip;
        #endregion

        #region Properties
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public Wallet Wallet { get { return _wallet; } set { _wallet = value; } }
        public IVehicle? IVehicle { get { return _vehicle; } set { _vehicle = value; } }
        public Country StartingCountry { get; }
        public Country CurrentCountry { get { return _currentCountry; } set { _currentCountry = value; } }
        public ITrip? Trip { get { return _trip; } set { _trip = value; } }
        public DateTime CreationTime { get; set; }
        #endregion

        #region Constructor
        public User(Country startingCountry)
        {
            StartingCountry = startingCountry;
            _vehicle = null;
            _trip = new Trip();
            _wallet = new Wallet();
            _currentCountry = new Country();
        }
        #endregion

        #region Methods
        public bool Drive(double distance, Country countryDestination)
        {
            var result = IVehicle.Drive(distance);

            if (result)
            {
                //Update Trip
                TripService.Instance.AddNewTripStep(distance, countryDestination, this);
            }

            return result;
        }

        public RefuelResults Refuel(double money)
        {
           return IVehicle.Refuel(money, CurrentCountry.GasPrice);
        }
        #endregion
        
    }
}
