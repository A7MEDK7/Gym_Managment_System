using Domin.Contract;
using Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications {
    public class BaseSpecifications<TEntity> : ISpecifications<TEntity> where TEntity : BaseEntity {
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria) {
            Criteria = criteria;
        }

        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; }
            = new List<Expression<Func<TEntity, object>>>();

        protected void AddIncludes(Expression<Func<TEntity,object>> includeExpressions)
            => IncludeExpression.Add(includeExpressions);
    }
}
