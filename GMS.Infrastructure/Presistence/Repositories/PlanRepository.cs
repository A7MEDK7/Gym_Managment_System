using Domin.Contract;
using Domin.GymEntities;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories {
    public class PlanRepository(GymDbContext _context) : IPlanRepository {
        public async Task<IEnumerable<Plan>> GetAll()
            => await _context.Set<Plan>().AsNoTracking().ToListAsync();

        public async Task<Plan?> GetById(int id)
            => await _context.Set<Plan>().FindAsync(id);

        public void Update(Plan plan)
            => _context.Set<Plan>().Update(plan);
    }
}
