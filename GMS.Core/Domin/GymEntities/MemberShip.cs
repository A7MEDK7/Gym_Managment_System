using Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.GymEntities {
    public class MemberShip : BaseEntity {
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;
        public DateTime EndDate { get; set; }
        public string Status {
            get {
                return EndDate >= DateTime.Now ? "Active" : "Expired";
            }
        }
    }
}
