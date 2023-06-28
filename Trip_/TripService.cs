using ApplicationNo1.Country_;
using ApplicationNo1.User_;

namespace ApplicationNo1.Trip_
{
    public sealed class TripService
    {
        private static TripService? _instance;
        private List<ITrip> _trips;

        private TripService() 
        {
            _trips = new List<ITrip>();
        }

        public static TripService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TripService();
                }
                return _instance;
            }
        }

        public List<ITrip> Trips { get { return _trips; } }

        public void AddNewTrip(ITrip trip)
        {
            _trips.Add(trip);
        }

        public void AddNewTripStep(double distance, Country countryDestination, IUser user)
        {
            if (_trips.Any(x => x.UserID == user.Id))
            {
                _trips.FirstOrDefault(x => x.UserID == user.Id).Steps.Add(new TripStep()
                {
                    CountryLanded = countryDestination,
                    DistanceTraveled = distance
                });
            }
            else
            {
                //Create new Trip with first default step 0 Distance and starting country
                _trips.Add(new Trip() 
                { 
                    UserID = user.Id,
                    UserVehicle = user.IVehicle,
                    Steps = new List<TripStep>() 
                    { 
                        new TripStep() 
                        { 
                            CountryLanded = user.StartingCountry, 
                            DistanceTraveled = 0 
                        },
                        new TripStep()
                        {
                            CountryLanded = countryDestination,
                            DistanceTraveled = distance
                        }
                    }
                });
            }
        }
    }
}
