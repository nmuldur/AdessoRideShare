using AdessoRideShare.DataAccess;
using AdessoRideShare.Entities;

namespace AdessoRideShare.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AdessoRideShareDbContext _context;
        private ITripRepository<Trip> _tripRepos;
        private ITripRepository<TripPassengers> _tripPassRepos;
        private ITripRepository<TripDriver> _tripDriverRepos;

        public UnitOfWork(AdessoRideShareDbContext context)
        {
            _context = context;
        }
        public ITripRepository<Trip> TripRepos
        {
            get { return _tripRepos ?? (_tripRepos = new TripRepository<Trip>(_context)); }
        }
        public ITripRepository<TripPassengers> TripPassRepos
        {
            get { return _tripPassRepos ?? (_tripPassRepos = new TripRepository<TripPassengers>(_context)); }
        }
        public ITripRepository<TripDriver> TripDriverRepos
        {
            get { return _tripDriverRepos ?? (_tripDriverRepos = new TripRepository<TripDriver>(_context)); }
        }

        // Bütün db commit ve rollback işemlerini tek bir yerden yönetmek için.
        public async Task Commit()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    _context.Dispose();
                    transaction.Rollback();
                }

            }

        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}
