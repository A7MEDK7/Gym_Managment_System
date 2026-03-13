using Domin.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data {
    public class DbInitilazer(GymDbContext _dbContext) : IDbInitilazer {
        public async Task InitilazeAsync() {
            // Check If Any Migration 
            try {
                if (_dbContext.Database.GetPendingMigrations().Any()) {
                    await _dbContext.Database.MigrateAsync();
                }
            } catch (Exception) {
                throw;
            }
            // Data Seeding
        }
    }
}
