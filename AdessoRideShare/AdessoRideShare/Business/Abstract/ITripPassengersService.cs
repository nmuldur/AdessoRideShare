using AdessoRideShare.Entities;

namespace AdessoRideShare.Business.Abstract
{
    public interface ITripPassengersService
    {
        Task<int> AddAsync(TripPassengers entity);
        Task<List<TripPassengers>> FindPassengersByTripIDAsync(int tripID);
    }
}
