using Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Contract {
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new() {
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? condition = null, bool asNoTracking = true); // GetAllAsync
        Task<TEntity?> GetAsync(int id); // GetByIdAsync
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity> specifications);  // Get All
        Task<TEntity?> GetAsync(ISpecifications<TEntity> specifications); // Get By Id
        Task AddAsync(TEntity entity); // AddAsync
        void Update(TEntity entity); // Update
        void Delete(TEntity entity); // Delete
    }
}
