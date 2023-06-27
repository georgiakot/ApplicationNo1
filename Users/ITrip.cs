using ApplicationNo1.Trip;
using ApplicationNo1.Users.Vehicles;

namespace ApplicationNo1.Users
{
    public interface ITrip
    {
        #region Properties
        string? UserID { get; }
        IVehicle? UserVehicle { get; set; }
        List<TripStep> Steps { get; set; }
        double TotalDistance { get; set; }
        double DistanceTraveled { get; set; }
        Country? CountryLanded { get; set; }
        #endregion

        #region Methods
        void AddNewStep(TripStep tripStep);
        #endregion

    }
}
