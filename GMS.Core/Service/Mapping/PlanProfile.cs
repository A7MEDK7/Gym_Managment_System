using AutoMapper;
using Domin.GymEntities;
using Shared.DTOs.PlanDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping {
    public class PlanProfile : Profile {
        public PlanProfile() {
            CreateMap<Plan, PlanDTO>();
            CreateMap<Plan, UpdatePlanDTO>();
        }
    }
}
