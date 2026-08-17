using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.Contract {
    public interface IServiceManger {
        public IMemberService MemberService { get; }
        public IAnalyticsService AnalyticsService { get; }
        public IPlanService PlanService { get; }
        public ISessionService SessionService { get; }
    }
}
