using Domin.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.MemberDTOs {
    public class HealthRecordDTO {
        [Required(ErrorMessage = "Height Is Required")]
        [Range(50, 300, ErrorMessage = "Height Must Be Between 50 And 300")]
        public decimal Height { get; set; }

        [Required(ErrorMessage = "Weight Is Required")]
        [Range(10, 400, ErrorMessage = "Weight Must Be Between 10 And 400")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "BloodType Is Required")]
        [StringLength(3, ErrorMessage = "BloodType Must Be 3 Letters Or Less")]
        public BloodType BloodType { get; set; }

        public string? Note { get; set; }
    }
}
