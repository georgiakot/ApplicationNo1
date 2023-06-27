using ApplicationNo1.Users.Vehicles;

namespace ApplicationNo1.Users
{
    public interface ITrip
    {
        #region Properties
        string? UserID { get; }
        IVehicle? UserVehicle { get; set; }
        List<ITrip> Steps { get; set; }
        double TotalDistance { get; set; }
        double DistanceTraveled { get; set; }
        Country? CountryLanded { get; set; }
        #endregion

        #region Methods
        void AddStepToList(ITrip step);
        #endregion

    }
}
