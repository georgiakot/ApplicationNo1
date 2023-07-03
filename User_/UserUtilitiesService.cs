using ApplicationNo1.Country_;
using ApplicationNo1.Trip_;
using ApplicationNo1.Vehicle_;
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
            else
            {
                return false;
            }
        }

        public RefuelResults Refuel(double orderForRefuelAmountInMoney, string userId)
        {
            var user = _userService.GetUserById(userId);
            var country = _userService.GetUserCurrentCountry(userId);

            return user.Vehicle.Refuel(orderForRefuelAmountInMoney, country.GasPrice);
        }

        public bool CheckBalance(double cash, string userId)
        {
            var user = _userService.GetUserById(userId);

            if (user != null) 
            {
                return user.Wallet.ChecksMoneyAvailable(cash);
            }
            else
            { 
                return false; 
            }
            
        }

        public void PaymentForFuel(string userId)
        {
            var user = _userService.GetUserById(userId);
            var country = _userService.GetUserCurrentCountry(userId);

            if (user != null && country != null)                
            {
                user.Wallet.Payment(user.Vehicle.RefuelAmount * country.GasPrice);
            }
        }
    }
}
