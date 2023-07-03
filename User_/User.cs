using ApplicationNo1.Country_;
using ApplicationNo1.Vehicle_;
using ApplicationNo1.Wallet_;

namespace ApplicationNo1.User_
{
    public class User : IUser
    {
        #region Fields
        protected IWallet? _iwallet;
        protected IVehicle? _ivehicle;
        protected Country? _currentCountry;
        protected Country? _startingCountry;
        #endregion

        #region Properties
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public IWallet? Wallet { get { return _iwallet; } set { _iwallet = value; } }
        public IVehicle? Vehicle { get { return _ivehicle; } set { _ivehicle = value; } }
        public Country StartingCountry { get { return _startingCountry; } }
        public Country CurrentCountry { get { return _currentCountry; } }
        public DateTime CreationTime { get; set; }
        #endregion

        #region Constructor
        public User(Country startingCountry)
        {                                     
            _startingCountry = startingCountry;
            _currentCountry = startingCountry;
        }
        #endregion

    }
}
