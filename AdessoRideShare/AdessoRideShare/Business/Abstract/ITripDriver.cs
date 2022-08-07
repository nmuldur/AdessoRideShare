using AdessoRideShare.Entities;

namespace AdessoRideShare.Business.Abstract
{
    public interface ITripDriver
    {
        Task<int> AddAsync(TripDriver entity);
    }
}
