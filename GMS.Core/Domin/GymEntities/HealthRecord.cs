using Domin.Entities;
using Domin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.GymEntities {
    public class HealthRecord : BaseEntity {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public BloodType BloodType { get; set; }
        public string? Note { get; set; }
    }
}
