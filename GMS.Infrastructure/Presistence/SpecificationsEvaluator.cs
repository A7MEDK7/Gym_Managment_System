using Domin.Contract;
using Domin.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence {
    internal static class SpecificationsEvaluator {
        public static IQueryable<TEntity> QetQuery<TEntity>(
            IQueryable<TEntity> inputQuery, // _dbcontext.set<Product>()
            ISpecifications<TEntity> specifications)
            where TEntity : BaseEntity {
            var query = inputQuery;

            if (specifications.Criteria is not null)
                query = query.Where(specifications.Criteria);
            // query = _dbcontext.set<Product>().Where(P => P.Id == 5)

            if (specifications.IncludeExpression?.Count() > 0) {
                foreach (var expression in specifications.IncludeExpression)
                    query = query.Include(expression);
                // query = _dbcontext.set<Entity>()
                // .Where(P => P.Id == 5)
                // .Include(P => P.Property01).Include(P => P.Property01)
            }
            return query;
        }
    }

}
