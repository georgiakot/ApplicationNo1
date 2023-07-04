using ApplicationNo1.Country_;
using ApplicationNo1.User_;

namespace ApplicationNo1.Trip_
{ 
    public interface ITripService
    {
        #region Properties
        List<ITrip> Trips { get; }
        #endregion

        #region Methods
        void AddNewTrip(ITrip trip);
        void AddNewTripStep(double distance, Country countryDestination, IUser user);
        #endregion
    }
}
