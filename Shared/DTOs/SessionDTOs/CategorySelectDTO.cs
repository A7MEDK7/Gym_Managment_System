using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.SessionDTOs {
    public class CategorySelectDTO {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
