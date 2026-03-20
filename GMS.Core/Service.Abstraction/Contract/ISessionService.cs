using Domin.GymEntities;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using Shared.DTOs.SessionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.Contract {
    public interface ISessionService {
        Task<IEnumerable<SessionDTO>> GetAllSessions();
        Task<SessionDTO?> GetSessionById(int sessionId);
        Task<bool> CreateSession(CreateSessionDTO createSessionDTO);
        Task<UpdateSessionDTO?> GetSessionToUpdate(int sessionId);
        Task<bool> UpdateSession(UpdateSessionDTO updateSessionDTO, int sessionId);
        Task<bool> RemoveSession(int sessionId);

    }
}
