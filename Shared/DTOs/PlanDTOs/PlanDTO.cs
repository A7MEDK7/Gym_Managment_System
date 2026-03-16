using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.PlanDTOs {
    public class PlanDTO {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Dsescription { get; set; } = null!;
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
