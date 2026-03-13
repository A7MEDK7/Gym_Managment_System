using Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.GymEntities {
    public class Member : GymUser {
        public string? Photo { get; set; }
        // JoinDate == CreatedAt Of BaseEntity

        // Relationships Member - HeathRecord
        public HealthRecord HealthRecord { get; set; } = null!;

        // Relationships Member - MemberSession
        public ICollection<MemberSession> MemberSession { get; set; } = null!;
    }
}
