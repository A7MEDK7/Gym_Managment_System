using Domin.GymEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Contract {
    public interface ISessionRepository : IGenericRepository<Session> {
        Task<IEnumerable<Session>> GetAllSessionsWithTrainerAndCategoryAsync();
        Task<Session?> GetSessionWithTrainerAndCategoryAsync(int sessionId);
        Task<int> GetCountOfBookedSlotsAsync(int sessionId);
    }
}
