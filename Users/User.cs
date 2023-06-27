using ApplicationNo1.Users.Vehicles;
using static ApplicationNo1.Users.Vehicles.VehicleBase;

namespace ApplicationNo1.Users
{
    public class User : IUser
    {
        #region Fields
        private Wallet? _wallet;
        private IVehicle? _vehicle;
        private Country? _currentCountry;
        private ITrip? _trip;
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

        #region Properties
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public Wallet Wallet { get { return _wallet; } set { _wallet = value; } }
        public IVehicle? Vehicle { get { return _vehicle; } set { _vehicle = value; } }
        public Country StartingCountry { get; }
        public Country CurrentCountry { get { return _currentCountry; } set { _currentCountry = value; } }
        public ITrip? Trip { get { return _trip; } set { _trip = value; } }
        public DateTime CreationTime { get; set; }
        #endregion

        #region Methods
        public bool Drive(double distance, Country countryDestination)
        {
            var result = Vehicle.Drive(distance);

            if (result)
            {
                //Update Trip
            }

            return result;
        }

        public RefuelResults Refuel(double money)
        {
           return Vehicle.Refuel(money, CurrentCountry.GasPrice);
        }
        #endregion
        public void NewTrip(Country country, double distance)
        {
            ITrip trip = new Trip()
            {
                CountryLanded = country,
                DistanceTraveled = distance
            };

            _trip.AddStepToList(trip);
        }
    }
}
