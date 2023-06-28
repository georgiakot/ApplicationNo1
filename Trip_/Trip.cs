using ApplicationNo1.Vehicle_;

namespace ApplicationNo1.Trip_
{
    public class Trip : ITrip
    {
        #region Fields
        private List<TripStep> _steps;
        #endregion

        #region Constructor
        public Trip()
        {
            _steps = new List<TripStep>();
        }
        #endregion

        #region Properties
        public string? UserID { get; set; }
        public IVehicle? UserVehicle { get; set; }
        public List<TripStep> Steps { get { return _steps; } set { _steps = value;  } }
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
    }
}
