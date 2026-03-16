using AutoMapper;
using Domin.Contract;
using Domin.GymEntities;
using Services.Abstraction.Contract;
using Shared.DTOs.PlanDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implmentations {
    public class PlanService(IUnitOfWork _unitOfWork, IMapper _mapper) : IPlanService {
        public async Task<IEnumerable<PlanDTO>> GetAllPlans() {
            var plans = await _unitOfWork.GetRepository<Plan>().GetAllAsync();
            if (plans is null || !plans.Any()) return Enumerable.Empty<PlanDTO>();
            return _mapper.Map<IEnumerable<PlanDTO>>(plans);
        }
        public async Task<PlanDTO?> GetPlanById(int planId) {
            var plan = await _unitOfWork.GetRepository<Plan>().GetAsync(planId);
            if (plan is null) return null;
            return _mapper.Map<PlanDTO>(plan);
        }
        public async Task<UpdatePlanDTO?> GetPanToUpdate(int planId) {
            var plan = await _unitOfWork.GetRepository<Plan>().GetAsync(planId);
            if (plan is null || await HasActiveMemberships(planId) || plan.IsActive == false) return null;
            return _mapper.Map<UpdatePlanDTO?>(plan);
        }
        public async Task<bool> UpdatePlan(int planId, UpdatePlanDTO updatedPlan) {
            var planRepo = _unitOfWork.GetRepository<Plan>();
            var plan = await planRepo.GetAsync(planId);
            if (plan is null || await HasActiveMemberships(planId)) return false;
            try {
                plan.Name = updatedPlan.Name;
                plan.Dsescription = updatedPlan.Dsescription;
                plan.DurationDays = updatedPlan.DurationDays;
                plan.Price = updatedPlan.Price;
                plan.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);
                planRepo.Update(plan);
                return await _unitOfWork.SaveChangesAsync() > 0;
            } catch (Exception) {
                return false;
            }
        }
        public async Task<bool> ToggleStatus(int planId) {
            var planRepo = _unitOfWork.GetRepository<Plan>();
            var plan = await planRepo.GetAsync(planId);
            if (plan is null || await HasActiveMemberships(planId)) return false;
            try {
                plan.IsActive = plan.IsActive == true ? false : true;
                plan.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);
                planRepo.Update(plan);
                return await _unitOfWork.SaveChangesAsync() > 0;
            } catch {
                return false;
            }
        }

        #region Helper Methods
        private async Task<bool> HasActiveMemberships(int planId) {
            var activeMemberships = await _unitOfWork.GetRepository<MemberShip>()
                                                     .GetAllAsync(X => X.PlanId == planId && X.Status == "Active");
            return activeMemberships.Any();
        }
        #endregion
    }
}
