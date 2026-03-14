using Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Contract {
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new() {
        Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = true); // GetAllAsync
        Task<TEntity?> GetAsync(int id); // GetByIdAsync
        Task AddAsync(TEntity entity); // AddAsync
        void Update(TEntity entity); // Update
        void Delete(TEntity entity); // Delete
    }
}
