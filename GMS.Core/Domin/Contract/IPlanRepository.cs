using Domin.GymEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Contract {
    public interface IPlanRepository {
        Task<Plan?> GetById(int id);
        Task<IEnumerable<Plan>> GetAll();
        void Update(Plan plan);
    }
}
