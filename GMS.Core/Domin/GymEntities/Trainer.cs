using Domin.Entities;
using Domin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.GymEntities {
    public class Trainer : GymUser {
        public Specialties Specialties { get; set; }
        // HireDate == CreatedAt Of BaseEntity

        // Realationship Trainer - Sessions
        public ICollection<Session> TrainerSessions { get; set; } = null!;
    }
}
