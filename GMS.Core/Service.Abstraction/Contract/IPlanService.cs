using Shared.DTOs.PlanDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.Contract {
    public interface IPlanService {
        Task<IEnumerable<PlanDTO>> GetAllPlans();
        Task<PlanDTO?> GetPlanById(int planId);
        Task<UpdatePlanDTO?> GetPanToUpdate(int planId);
        Task<bool> UpdatePlan(int planId, UpdatePlanDTO updatedPlan);
        Task<bool> ToggleStatus(int planId);
    }
}
