using ApplicationNo1.Users.Vehicles;

namespace ApplicationNo1.Trip
{
    public class Trip : ITrip
    {
        #region Fields
        private double _totalDistance;
        private double _distanceTraveled;
        private List<TripStep> _steps;
        #endregion

        #region Properties
        public string? UserID { get; }
        public IVehicle? UserVehicle { get; set; }
        public List<TripStep> Steps { get { return _steps; } }
        public double TotalDistance
        {
            get
            {
                double sum = 0;
                foreach (var step in Steps)
                {
                    sum += step.DistanceTraveled;
                }
                return sum;
            }
        }

        #endregion

        #region Constructor
        public Trip()
        {
            _steps = new List<TripStep>();
            _totalDistance = 0;
            _distanceTraveled = 0;
        }
        #endregion

        #region Methods
        public void AddNewStep(TripStep tripStep)
        {
            _steps.Add(tripStep);
        }
        #endregion

    }
}
