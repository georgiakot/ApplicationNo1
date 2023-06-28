using ApplicationNo1.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationNo1.Trips
{
    public sealed class TripService
    {
        private TripService() 
        {
            _trips = new List<Trip>();
        }

        private static TripService _instance = null;

        private List<Trip> _trips;

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

        public List<Trip> Trips { get { return _trips; } }

        public void AddNewTrip(Trip trip)
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
