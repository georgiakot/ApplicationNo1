using ApplicationNo1.Vehicle_;

namespace ApplicationNo1.Trip_
{
    public interface ITrip
    {
        #region Properties
        string? UserID { get; }
        IVehicle? UserVehicle { get; set; }
        List<TripStep> Steps { get; set; }
        double TotalDistance { get; }
        #endregion
    }
}
