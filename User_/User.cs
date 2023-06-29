﻿using ApplicationNo1.Country_;
using ApplicationNo1.Trip_;
using ApplicationNo1.Vehicle_;
using ApplicationNo1.Wallet_;
using static ApplicationNo1.Vehicle_.VehicleBase;

namespace ApplicationNo1.User_
{
    public class User : IUser
    {
        #region Fields
        private IWallet? _iwallet;
        private IVehicle? _ivehicle;
        private Country? _currentCountry;
        private Country? _startingCountry;
        #endregion

        #region Properties
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public IWallet? IWallet { get { return _iwallet; } set { _iwallet = value; } }
        public IVehicle? IVehicle { get { return _ivehicle; } set { _ivehicle = value; } }
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

        #region Methods
        public bool Drive(double distance, Country countryDestination)
        {
            var result = _ivehicle.Drive(distance);

            if (result)
            {
                //Update Trip
                TripService.Instance.AddNewTripStep(distance, countryDestination, this);

                //Update Current Country
                _currentCountry = countryDestination;
            }

            return result;
        }

        public RefuelResults Refuel(double money)
        {
           return _ivehicle.Refuel(money, CurrentCountry.GasPrice);
        }

        public bool CheckBalance(double cash)
        {
            return _iwallet.ChecksMoneyAvailable(cash);
        }

        public void Payment(double cash)
        {
            _iwallet.Payment(cash);
        }

        public void PaymentWithChange(double cash,double moneyRequired)
        {
            _iwallet.GivesChange(cash, moneyRequired);
        }
        #endregion
        
    }
}
