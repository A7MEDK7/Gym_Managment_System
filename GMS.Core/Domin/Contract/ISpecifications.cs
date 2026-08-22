using Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Contract {
    public interface ISpecifications<TEntity> where TEntity : BaseEntity {
        // Property Signature For Each & Every Spec [Where, Include, etc ..]
        // Criteria --> where(P => P.Id)
        public Expression<Func<TEntity, bool>>? Criteria { get; }  // P => P.Id

        // Include --> Include(P => P.Property) [May Be More Than One Include]
        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; }
    }
}
