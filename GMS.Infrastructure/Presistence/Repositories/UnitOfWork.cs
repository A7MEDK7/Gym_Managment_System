using Domin.Contract;
using Domin.Entities;
using Presistence.Data;
using System.Collections.Concurrent;

namespace Presistence.Repositories {
    public class UnitOfWork(GymDbContext _context) : IUnitOfWork {

        private readonly ConcurrentDictionary<String, object> _repositories = new();

        public async Task<int> SaveChangesAsync()  => await _context.SaveChangesAsync();
        
        public IPlanRepository GetPlanRepository()
            => (IPlanRepository)_repositories.GetOrAdd(typeof(IPlanRepository).Name,
                key => new PlanRepository(_context));

        public ISessionRepository GetSessionRepository()
            =>  (ISessionRepository) _repositories.GetOrAdd(typeof(ISessionRepository).Name,
                key => new SessionRepository(_context));

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
            => (IGenericRepository<TEntity>)_repositories.GetOrAdd(typeof(TEntity).Name, 
                key => new GenericRepository<TEntity>(_context));
    }
}
