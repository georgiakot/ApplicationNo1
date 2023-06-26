using ApplicationNo1.Users.Vehicles;

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
        /*

        public double Balance { get { return _wallet.Balance; } set { _wallet.Balance = value; } }

        public string? Currency { get { return _country.Currency; } set { _country.Currency = value; } }

        public  double KmCounter { get { return _vehicle.KmCounter; } set { _vehicle.KmCounter = value; } }

        public string? CountryName { get { return _country.Name; } set { _country.Name = value; } }

        */
      

        public string? Name { get; set; }
        public int Age { get; set; }
        public Wallet Wallet { get { return _wallet; } set { _wallet = value; } }
        public IVehicle? Vehicle { get { return _vehicle; } set { _vehicle = value; } }
        public Country Country { get { return _country; } set { _country = value; } }
        public DateTime CreationTime { get; set; }
        #endregion

    }
}
