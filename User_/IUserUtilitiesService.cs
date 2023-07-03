using ApplicationNo1.Country_;
using static ApplicationNo1.Vehicle_.VehicleBase;

namespace ApplicationNo1.User_
{ 
    public interface IUserUtilitiesService
    {
        bool Drive(double distance, Country countryDestination, string userId);
        RefuelResults Refuel(double orderForRefuelAmountInMoney,string userId);
        bool CheckBalance(double cash, string userId);
        void PaymentForFuel(string userId);
    }
}
