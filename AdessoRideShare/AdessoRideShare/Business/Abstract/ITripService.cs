using AdessoRideShare.Entities;
using AdessoRideShare.Models;

namespace AdessoRideShare.Business.Abstract
{
    public interface ITripService
    {
        Task<int> AddAsync(Trip entity);
        Task<WebApiResponse> UpdateAsync(int? tripId, Trip entity);
        Task<IEnumerable<Trip>> GetAsync();
        Task<Trip> GetByIdAsync(int? tripId);
        Task<List<Trip>> SearchTripByAddress(SearchTripRequest searchTripRequest);
    }
}
