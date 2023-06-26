using ApplicationNo1.Users.Vehicles;
using static ApplicationNo1.Users.Vehicles.VehicleBase;

namespace ApplicationNo1.Users
{
    public class User : IUser
    {
        #region Fields
        private Wallet? _wallet;
        private IVehicle? _vehicle;
        private Country _country;
        #endregion

        #region Constructor
        public User()
        {
            _vehicle = null;
            _wallet = new Wallet();
            _country = new Country();
        }
        #endregion

        #region Properties
        public string? Name { get; set; }
        public int Age { get; set; }
        public Wallet Wallet { get { return _wallet; } set { _wallet = value; } }
        public IVehicle? Vehicle { get { return _vehicle; } set { _vehicle = value; } }
        public Country Country { get { return _country; } set { _country = value; } }
        public DateTime CreationTime { get; set; }
        #endregion

        #region Methods
        public bool UserDrive(double distance)
        {
            return Vehicle.Drive(distance);
        }

        public RefuelResults UserRefuel(double money)
        {

           return Vehicle.Refuel(money, Country.GasPrice);

        }
        #endregion

    }
}
