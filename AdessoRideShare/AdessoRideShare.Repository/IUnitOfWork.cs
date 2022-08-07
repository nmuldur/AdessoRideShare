using AdessoRideShare.Entities;

namespace AdessoRideShare.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ITripRepository<Trip> TripRepos { get; }
        ITripRepository<TripPassengers> TripPassRepos { get; }
        ITripRepository<TripDriver> TripDriverRepos { get; }
        Task Commit();

    }
}
