using AutoMapper;
using Domin.Contract;
using Services.Abstraction.Contract;

namespace Services.Implmentations {
    public class ServiceManger(IUnitOfWork unitOfWork, IMapper mapper) : IServiceManger {
        private readonly Lazy<ISessionService> _sessionService = new Lazy<ISessionService>(() => new SessionService(unitOfWork, mapper));
        private readonly Lazy<IMemberService> _memberService = new Lazy<IMemberService>(() => new MemberService(unitOfWork, mapper));
        private readonly Lazy<ITrainerService> _trainerService = new Lazy<ITrainerService>(() => new TrainerService(unitOfWork, mapper));
        private readonly Lazy<IAnalyticsService> _analyticsService = new Lazy<IAnalyticsService>(() => new AnalyticsService(unitOfWork));
        private readonly Lazy<IPlanService> _planService = new Lazy<IPlanService>(() => new PlanService(unitOfWork, mapper));
        public ISessionService SessionService => _sessionService.Value;
        public IMemberService MemberService => _memberService.Value;
        public ITrainerService TrainerService => _trainerService.Value;
        public IAnalyticsService AnalyticsService => _analyticsService.Value;
        public IPlanService PlanService => _planService.Value;
    }
}
