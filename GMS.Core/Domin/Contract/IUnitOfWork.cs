using Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Contract {
    public interface IUnitOfWork {
        Task<int> SaveChangesAsync();
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();
        IPlanRepository GetPlanRepository();
    }
}
