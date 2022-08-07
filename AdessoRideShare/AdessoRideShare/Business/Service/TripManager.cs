using AdessoRideShare.Business.Abstract;
using AdessoRideShare.Entities;
using AdessoRideShare.Models;
using AdessoRideShare.Repository;
using System.Net;

namespace AdessoRideShare.Business.Service
{
    public class TripManager : ITripService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<TripManager> _logger;

        public TripManager(IUnitOfWork unit, ILogger<TripManager> logger)
        {
            _uow = unit;
            _logger = logger;
        }

        public async Task<int> AddAsync(Trip trip)
        {
            try
            {
                await _uow.TripRepos.AddAsync(trip);
                await _uow.Commit();
                return trip.ID;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Seyahat eklenirken sorun oluştu.", trip);
                throw e;
            }

        }
        public async Task<WebApiResponse> UpdateAsync(int? tripId, Trip pTrip)
        {
            try
            {
                var trip = await _uow.TripRepos.GetByIdAsync(tripId);
                if (trip != null)
                {

                    trip.FromAddress = pTrip.FromAddress;
                    trip.ToAddress = pTrip.ToAddress;
                    trip.TripDate = pTrip.TripDate;
                    trip.Description = pTrip.Description;
                    trip.SeatCapacity = pTrip.SeatCapacity;
                    trip.IsLive = pTrip.IsLive;

                    await _uow.TripRepos.UpdateAsync(trip);
                    await _uow.Commit();

                    return new WebApiResponse()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = true
                    };
                }
                else
                {
                    return new WebApiResponse()
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Status = false
                    };
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Seyahat güncellenirken sorun oluştu.", pTrip);
                throw e;
            }
        }
        public async Task<IEnumerable<Trip>> GetAsync()
        {
            try
            {
                return await _uow.TripRepos.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Trip> GetByIdAsync(int? tripId)
        {
            try
            {
                var result = await _uow.TripRepos.GetByIdAsync(tripId);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Trip>> SearchTripByAddress(SearchTripRequest searchTripRequest)
        {
            try
            {
                List<Trip> tripPassengerList = null;
                tripPassengerList = await _uow.TripRepos.SearchByAsync(x => x.FromAddress == searchTripRequest.FromAddress && x.ToAddress == x.ToAddress);
                await _uow.Commit();
                return tripPassengerList;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Seyahat aranırken sorun oluştu.", searchTripRequest);
                throw e;
            }
        }
    }
}
