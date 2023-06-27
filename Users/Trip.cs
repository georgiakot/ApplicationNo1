using ApplicationNo1.Users.Vehicles;

namespace ApplicationNo1.Users
{
    public class Trip : ITrip
    {
        #region Fields
        private double _totalDistance;
        private double _distanceTraveled;
        private List<ITrip> _steps;
        #endregion

        #region Constructor
        public Trip()
        {
            _steps = new List<ITrip>();
            _totalDistance = 0;
            _distanceTraveled = 0;
        }
        #endregion

        #region Properties
        public string? UserID { get; }
        public IVehicle? UserVehicle { get; set; }
        public List<ITrip> Steps { get { return _steps; } set { _steps = value; } }
        public double TotalDistance { get { return _totalDistance; } set { _totalDistance = value; } }

        public double DistanceTraveled { get { return _distanceTraveled; } set { _distanceTraveled = value; } } 
        public Country? CountryLanded { get; set; }

        #endregion

        #region Methods
        public void AddStepToList(ITrip step)
        {
            _steps.Add(step);
        }
        #endregion

    }
}
