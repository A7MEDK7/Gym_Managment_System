using Domin.Contract;
using Domin.Entities;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories {
    public class GenericRepository<TEntity>(GymDbContext _context) : IGenericRepository<TEntity> where TEntity : BaseEntity, new() {
        
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = true)
            => asNoTracking? await _context.Set<TEntity>().AsNoTracking().ToListAsync() : 
                                 await _context.Set<TEntity>().ToListAsync();
        
        public async Task<TEntity?> GetAsync(int id) 
            => await _context.Set<TEntity>().FindAsync(id);

        public async Task AddAsync(TEntity entity)
            => await _context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) 
            => _context.Set<TEntity>().Update(entity);
    }
}
