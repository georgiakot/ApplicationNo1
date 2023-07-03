using ApplicationNo1.Country_;
using ApplicationNo1.Trip_;
using static ApplicationNo1.Vehicle_.VehicleBase;

namespace ApplicationNo1.User_
{ 
    public class UserUtilitiesService : User, IUserUtilitiesService
    {
        private ITripService _tripService;

        public UserUtilitiesService(Country startingCountry,ITripService tripService) : base(startingCountry)
        {
            _tripService = tripService;
        }


        public bool Drive(double distance, Country countryDestination)
        {
            var result = _ivehicle.Drive(distance);

            if (result)
            {
                //Update Trip
                _tripService.AddNewTripStep(distance, countryDestination, this);

                //Update Current Country
                _currentCountry = countryDestination;
            }
            return result;
        }

        public RefuelResults Refuel(double orderForRefuelAmountInMoney)
        {
            return _ivehicle.Refuel(orderForRefuelAmountInMoney, CurrentCountry.GasPrice);
        }

        public bool CheckBalance(double cash)
        {
            return _iwallet.ChecksMoneyAvailable(cash);
        }

        public void PaymentForFuel()
        {
            _iwallet.Payment(_ivehicle.RefuelAmount * _currentCountry.GasPrice);
        }
    }
}
