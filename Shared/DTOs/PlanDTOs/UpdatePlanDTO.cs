using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.PlanDTOs {
    public class UpdatePlanDTO {
        [Required(ErrorMessage = "Plan Name Is Required")]
        [StringLength(50, ErrorMessage = "Plan Name Must Be Less Than 51 Char")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description Is Required")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Description Must Be Between 5 and 200 Char")]
        public string Dsescription { get; set; } = null!;

        [Required(ErrorMessage = "DurationDays Is Required")]
        [Range(1, 365, ErrorMessage = "Duration Days Must Be Between 1 and 365 Char")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "Price Is Required")]
        public decimal Price { get; set; }
    }
}
