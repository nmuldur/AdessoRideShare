using AdessoRideShare.Business.Abstract;
using AdessoRideShare.Entities;
using AdessoRideShare.Repository;

namespace AdessoRideShare.Business.Service
{
    public class TripDriverManager : ITripDriver
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<TripDriverManager> _logger;
        public TripDriverManager(IUnitOfWork unit, ILogger<TripDriverManager> logger)
        {
            _uow = unit;
            _logger = logger;
        }

        public async Task<int> AddAsync(TripDriver tripDriver)
        {
            try
            {
                await _uow.TripDriverRepos.AddAsync(tripDriver);
                await _uow.Commit();
                return tripDriver.ID;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Seyahate şoför eklenirken sorun oluştu.", tripDriver);
                throw e;
            }
        }
    }
}
