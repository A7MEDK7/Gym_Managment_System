using AutoMapper;
using Domin.GymEntities;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using Shared.DTOs.SessionDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping {
    public class SessionProfile : Profile {
        public SessionProfile() {
            CreateMap<Session, SessionDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.SessionCategory.CategoryName))
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.SessionTrainer.Name))
                .ForMember(dest => dest.AvailableSlots, opt => opt.Ignore());

            CreateMap<CreateSessionDTO, Session>();

            CreateMap<UpdateSessionDTO, Session>().ReverseMap();
        }

    }
}