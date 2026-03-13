using Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.GymEntities {
    public class Category : BaseEntity {
        public string CategoryName { get; set; } = null!;

        // Realationship Category - Session
        public ICollection<Session> Sessions { get; set; } = null!;
    }
}
