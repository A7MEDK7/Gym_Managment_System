using Domin.Contract;
using Domin.GymEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
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

            // Data Seeding --> Categories
            if (!_dbContext.Categories.Any()) {
                var categoriesData = LoadDataFromJsonFile<Category>("categories.json");
                if (categoriesData.Any()) _dbContext.Categories.AddRange(categoriesData);
                _dbContext.SaveChanges();
            }

            // Data Seeding --> Plans
            if (!_dbContext.Plans.Any()) {
                var plansData = LoadDataFromJsonFile<Plan>("plans.json");
                if (plansData.Any()) _dbContext.Plans.AddRange(plansData);
                _dbContext.SaveChanges();
            }
        }


        private List<T> LoadDataFromJsonFile<T>(string fileName) {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Data", fileName);
            if (!File.Exists(filePath)) throw new FileNotFoundException();
            string data = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions() {
                PropertyNameCaseInsensitive = true,
            };
            return JsonSerializer.Deserialize<List<T>>(data, options) ?? new List<T>();
        }
    }
}
