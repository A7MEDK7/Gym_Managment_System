using AutoMapper;
using Domin.Entities;
using Domin.GymEntities;
using Shared.DTOs.MemberDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping {
    public class MemberProfile : Profile {
        public MemberProfile() {

            CreateMap<Member, MemberDTO>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()));

            CreateMap<CreateMemberDTO, Member>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address {
                    BuildingNumber = src.BuildingNumber,
                    City = src.City,
                    Street = src.Street
                }))
                .ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => new HealthRecord {
                Height = src.HealthRecordDTO.Height,
                Weight = src.HealthRecordDTO.Weight,
                BloodType = src.HealthRecordDTO.BloodType,
                Note = src.HealthRecordDTO.Note
            }));

            CreateMap<Member, MemberDetailsDTO>()
                // Gender Enum → String
                .ForMember(dest => dest.Gender,
                    opt => opt.MapFrom(src => src.Gender.ToString()))
                // DateOfBirth
                .ForMember(dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth.ToString("yyyy-MM-dd")))
                // Address
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                    $"({src.Address.BuildingNumber}) - {src.Address.Street} - {src.Address.City}"))
                // Ignore Properties Which Will Handels In MemberService
                .ForMember(dest => dest.PlanName, opt => opt.Ignore())
                .ForMember(dest => dest.MemberShipStartDate, opt => opt.Ignore())
                .ForMember(dest => dest.MemberShipEndDate, opt => opt.Ignore());

            CreateMap<HealthRecord, HealthRecordDTO>();

            CreateMap<MemberToUpdateDTO, Member>()
                .ForPath(dest => dest.Address.BuildingNumber,
                    opt => opt.MapFrom(src => src.BuildingNumber))
                .ForPath(dest => dest.Address.City,
                    opt => opt.MapFrom(src => src.City))
                .ForPath(dest => dest.Address.Street,
                    opt => opt.MapFrom(src => src.Street))

                .ForPath(dest => dest.HealthRecord.Weight,
                    opt => opt.MapFrom(src => src.HealthRecordDTO.Weight))
                .ForPath(dest => dest.HealthRecord.Height,
                    opt => opt.MapFrom(src => src.HealthRecordDTO.Height))
                .ForPath(dest => dest.HealthRecord.BloodType,
                    opt => opt.MapFrom(src => src.HealthRecordDTO.BloodType))
                .ForPath(dest => dest.HealthRecord.Note,
                    opt => opt.MapFrom(src => src.HealthRecordDTO.Note))

                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .ForMember(dest => dest.Gender, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth, opt => opt.Ignore())
                .ForMember(dest => dest.MemberSession, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()).ReverseMap();
        }
    }
}
