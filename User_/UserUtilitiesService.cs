using ApplicationNo1.Country_;
using ApplicationNo1.Trip_;
using static ApplicationNo1.Vehicle_.VehicleBase;

namespace ApplicationNo1.User_
{ 
    public class UserUtilitiesService : IUserUtilitiesService
    {
        private ITripService _tripService;
        private IUserService _userService;

        public UserUtilitiesService(ITripService tripService, IUserService userService)
        {
            _tripService = tripService;
            _userService = userService;
        }


        public bool Drive(double distance, Country countryDestination, string userId)
        {
            var user = _userService.GetUserById(userId);
            if (user != null)
            {
                var result = user.Vehicle.Drive(distance);

                if (result)
                {
                    //Update Trip
                    _tripService.AddNewTripStep(distance, countryDestination, user);
                }
                return result;
            }

            return false;

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
