using Domin.GymEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications {
    public class MemberWithHealthRecordSpecification : BaseSpecifications<Member> {
        public MemberWithHealthRecordSpecification(int id) : base(P => P.Id == id) {
            AddIncludes(P => P.HealthRecord);
        }
        public MemberWithHealthRecordSpecification() : base(null) {
            AddIncludes(P => P.HealthRecord);
        }
    }
}
