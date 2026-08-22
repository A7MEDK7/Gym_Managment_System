using Domin.GymEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications {
    public class MemberWitSessionSpecification : BaseSpecifications<Member> {
        public MemberWitSessionSpecification(int id) : base(P => P.Id == id) {
            AddIncludes(P => P.HealthRecord);
        }
        public MemberWitSessionSpecification() : base(null) {
            AddIncludes(P => P.HealthRecord);
        }
    }
}
