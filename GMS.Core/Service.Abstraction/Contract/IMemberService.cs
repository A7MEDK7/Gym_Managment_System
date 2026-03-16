using Shared.DTOs.MemberDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.Contract {
    public interface IMemberService {
        Task<IEnumerable<MemberDTO>> GetAllMembers();
        Task<bool> CreateMember(CreateMemberDTO createMemberDTO);
        Task<MemberDetailsDTO?> GetMemberDetailsById(int memberId);
        Task<HealthRecordDTO?> GetMemberHealthRecordDTO(int memberId);
        Task<MemberToUpdateDTO?> GetMemberToUpdate(int memberId);
        Task<bool> UpdateMemberDetails(int memberId, MemberToUpdateDTO memberToUpdateDTO);
        Task<bool> RemoveMember(int memberId);
    }
}
