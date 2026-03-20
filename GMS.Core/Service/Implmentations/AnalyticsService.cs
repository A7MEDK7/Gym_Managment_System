using Domin.Contract;
using Domin.GymEntities;
using Services.Abstraction.Contract;
using Shared.DTOs.AnalyticsDTOs;

namespace Services.Implmentations {
    public class AnalyticsService(IUnitOfWork _unitOfWork) : IAnalyticsService {
        public async Task<AnalyticDTO> GetAnalyticData() {
            // Get All Data
            var ActiveMembers = await _unitOfWork.GetRepository<MemberShip>().GetAllAsync(X => X.EndDate >= DateTime.Now);
            var TotalMembers = await _unitOfWork.GetRepository<Member>().GetAllAsync();
            var TotalTrainers = await _unitOfWork.GetRepository<Trainer>().GetAllAsync();
            var sessions = _unitOfWork.GetRepository<Session>();
            var UpcomingSessions = await sessions.GetAllAsync(X => X.StartDate > DateTime.Now);
            var OngoingSessions = await sessions.GetAllAsync(X => X.StartDate <= DateTime.Now && X.EndDate >= DateTime.Now);
            var CompletedSessions = await sessions.GetAllAsync(X => X.EndDate < DateTime.Now);

            // Return Data With DTO
            return new AnalyticDTO() {
                ActiveMembers = ActiveMembers.Count(),
                TotalMembers = TotalMembers.Count(),
                TotalTrainers = TotalTrainers.Count(),
                UpcomingSessions = UpcomingSessions.Count(),
                OngoingSessions = OngoingSessions.Count(),
                CompletedSessions = CompletedSessions.Count()
            };
        }
    }
}
