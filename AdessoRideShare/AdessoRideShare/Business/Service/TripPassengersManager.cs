using AdessoRideShare.Business.Abstract;
using AdessoRideShare.Entities;
using AdessoRideShare.Repository;

namespace AdessoRideShare.Business.Service
{
    public class TripPassengersManager : ITripPassengersService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<TripPassengersManager> _logger;
        public TripPassengersManager(IUnitOfWork unit, ILogger<TripPassengersManager> logger)
        {
            _uow = unit;
            _logger = logger;
        }
        public async Task<int> AddAsync(TripPassengers tripPassenger)
        {
            try
            {
                await _uow.TripPassRepos.AddAsync(tripPassenger);
                await _uow.Commit();
                return tripPassenger.ID;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Seyahate yolcu eklenirken sorun oluştu.", tripPassenger);
                throw e;
            }
        }

        public async Task<List<TripPassengers>> FindPassengersByTripIDAsync(int tripID)
        {
            try
            {
                List<TripPassengers> tripPassengerList = null;
                tripPassengerList =  await _uow.TripPassRepos.SearchByAsync(x=> x.TripID==tripID);
                await _uow.Commit();
                return tripPassengerList;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Yolcu aranırken sorun oluştu.", tripID);
                throw e;
            }
        }
    }
}
