using Domin.Contract;
using Domin.Entities;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories {
    public class GenericRepository<TEntity>(GymDbContext _context) : IGenericRepository<TEntity> where TEntity : BaseEntity, new() {

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? condition = null, bool asNoTracking = true) {
            // Initialize Excutable Query
            IQueryable<TEntity> query = _context.Set<TEntity>();
            // Check Tracking Conditon
            if (asNoTracking) query = query.AsNoTracking();
            // Check Filtering Conditon
            if (condition is not null) query = query.Where(condition);
            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetAsync(int id) 
            => await _context.Set<TEntity>().FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity> specifications)
            => await SpecificationsEvaluator.QetQuery(_context.Set<TEntity>(), specifications).ToListAsync();

        public async Task<TEntity?> GetAsync(ISpecifications<TEntity> specifications)
            => await SpecificationsEvaluator.QetQuery(_context.Set<TEntity>(), specifications).FirstOrDefaultAsync();

        public async Task AddAsync(TEntity entity)
            => await _context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) 
            => _context.Set<TEntity>().Update(entity);
    }
}
