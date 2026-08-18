using AutoMapper;
using AutoMapper.Execution;
using Domin.Contract;
using Domin.GymEntities;
using Services.Abstraction.Contract;
using Shared.DTOs.MemberDTOs;
using Member = Domin.GymEntities.Member;

namespace Services.Implmentations {
    public class MemberService(IUnitOfWork _unitOfWork, IMapper _mapper) : IMemberService {
        public async Task<IEnumerable<MemberDTO>> GetAllMembers() {
            try {
                var members = await _unitOfWork.GetRepository<Member>().GetAllAsync();
                // Check For Exsiting Members
                if (members is null || !members.Any()) return Enumerable.Empty<MemberDTO>();
                // Mapping From Member To MemberDTO
                var memberResult = _mapper.Map<IEnumerable<MemberDTO>>(members);
                return memberResult;
            } catch (Exception) {
                return Enumerable.Empty<MemberDTO>();
            }
        }
        public async Task<MemberDetailsDTO?> GetMemberDetailsById(int memberId) {
            try {
                var membersRepo = _unitOfWork.GetRepository<Member>();
                var member = await membersRepo.GetAsync(memberId);
                // Check If Member Is Null
                if (member is null) return null;
                // Mapping From Member To MemberDetailsDTO
                var memberResult = _mapper.Map<MemberDetailsDTO>(member);

                //var memberShip = await _unitOfWork.GetRepository<MemberShip>().GetAllAsync(X => X.MemberId == memberId && X.Status == "Active");

                // Compute 'now' Once So EF Can Translate The Comparison To SQL (Parameterized)
                var now = DateTime.Now;

                var memberShip = await _unitOfWork.GetRepository<MemberShip>()
                                                  .GetAllAsync(x => x.MemberId == memberId && x.EndDate >= now);

                var activeMemberShip = memberShip.FirstOrDefault();
                // Assign Dates To Member
                if (activeMemberShip is not null) {
                    memberResult.MemberShipStartDate = activeMemberShip.CreatedAt.ToString();
                    memberResult.MemberShipEndDate = activeMemberShip.EndDate.ToString() ?? "N/A";
                    // Get Member Plan 
                    var memberPlan = await _unitOfWork.GetPlanRepository().GetById(activeMemberShip.PlanId);
                    memberResult.PlanName = memberPlan?.Name ?? "No Plan";
                }
                return memberResult;
            } catch (Exception) {
                return null;
            }
        }
        public async Task<bool> CreateMember(CreateMemberDTO createMemberDTO) {
            try {
                var memberRepo = _unitOfWork.GetRepository<Member>();
                if (await IsEmailExist(createMemberDTO.Email) || await IsPhoneExist(createMemberDTO.Phone)) return false;
                // Add New Member
                var member = _mapper.Map<Member>(createMemberDTO);
                await _unitOfWork.GetRepository<Member>().AddAsync(member);
                return await _unitOfWork.SaveChangesAsync() > 0;
            } catch (Exception) {
                return false;
            }
        }
        public async Task<HealthRecordDTO?> GetMemberHealthRecordDTO(int memberId) {
            try {
                var healthRecordRepo = _unitOfWork.GetRepository<HealthRecord>();
                var memberHealthRecord = await healthRecordRepo.GetAsync(memberId);
                // Check If Member Health Record Is Null
                if (memberHealthRecord is null) return null;
                // Mapping From HealthRecord To HealthRecordDTO
                var heathRecordResult = _mapper.Map<HealthRecordDTO>(memberHealthRecord);
                return heathRecordResult;
            } catch (Exception) {
                return null;
            }
        }
        public async Task<MemberToUpdateDTO?> GetMemberToUpdate(int memberId) {
            var member = await _unitOfWork.GetRepository<Member>().GetAsync(memberId);
            if(member is null) return null;
            var memberResult = _mapper.Map<MemberToUpdateDTO>(member);
            return memberResult;
        }
        public async Task<bool> UpdateMemberDetails(int memberId, MemberToUpdateDTO memberToUpdateDTO) {
            try {
                // Gat Repository
                var memberRepo = _unitOfWork.GetRepository<Member>();
                // Check If User Insert Email or Phone Is Already Exist
                if (await IsEmailExist(memberId, memberToUpdateDTO.Email) || await IsPhoneExist(memberId, memberToUpdateDTO.Phone)) return false;
                // Get The Member From Database And Check
                var memberToUpdate = await memberRepo.GetAsync(memberId);
                if (memberToUpdate is null) return false;
                // Update The Member Details
                memberToUpdate.Name = memberToUpdateDTO.Name;
                memberToUpdate.Email = memberToUpdateDTO.Email;
                memberToUpdate.Phone = memberToUpdateDTO.Phone;
                memberToUpdate.Address.BuildingNumber = memberToUpdateDTO.BuildingNumber;
                memberToUpdate.Address.Street = memberToUpdateDTO.Street;
                memberToUpdate.Address.City = memberToUpdateDTO.City;
                memberToUpdate.HealthRecord.Height = memberToUpdateDTO.HealthRecordDTO.Height;
                memberToUpdate.HealthRecord.Weight = memberToUpdateDTO.HealthRecordDTO.Weight;
                memberToUpdate.HealthRecord.BloodType = memberToUpdateDTO.HealthRecordDTO.BloodType;
                memberToUpdate.HealthRecord.Note = memberToUpdateDTO.HealthRecordDTO.Note;
                memberToUpdate.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);
                // Update Member In Database
                memberRepo.Update(memberToUpdate);
                return await _unitOfWork.SaveChangesAsync() > 0;
            } catch (Exception) {
                return false;
            }

        }
        public async Task<bool> RemoveMember(int memberId) {
            var memberRepo = _unitOfWork.GetRepository<Member>();
            var member = await memberRepo.GetAsync(memberId);
            if (member is null) return false;
            // Get Member Sessions To Check That The Selected Member Does Not Has Any Booking Session
            var hasActiveMemberSessions = await _unitOfWork.GetRepository<MemberSession>()
                                                        .GetAllAsync(X => X.MemberId == memberId && X.Session.StartDate > DateTime.Now);
            if (hasActiveMemberSessions is not null) return false;
            // Get MemberShips To Delete It Then Delete The Member
            var memberShips = await _unitOfWork.GetRepository<MemberShip>().GetAllAsync(X => X.MemberId == memberId);
            try {
                if (memberShips.Any()) {
                    foreach (var membership in memberShips) {
                        _unitOfWork.GetRepository<MemberShip>().Delete(membership);
                    }
                }
                _unitOfWork.GetRepository<Member>().Delete(member);
                return await _unitOfWork.SaveChangesAsync() > 0;
            } catch (Exception) {
                return false;
            }
        }
        
        #region Helper Methods
        private async Task<bool> IsEmailExist(int memberId, string email) {
            var memberRepo = _unitOfWork.GetRepository<Member>();
            // Check If User Email Already Exist
            var memberEmail = await memberRepo.GetAllAsync(m => m.Email == email && m.Id != memberId);
            return memberEmail.Any();
        }
        private async Task<bool> IsEmailExist(string email) {
            var memberRepo = _unitOfWork.GetRepository<Member>();
            // Check If User Email Already Exist
            var memberEmail = await memberRepo.GetAllAsync(m => m.Email == email);
            return memberEmail.Any();
        }
        private async Task<bool> IsPhoneExist(int memberId, string phone) {
            var memberRepo = _unitOfWork.GetRepository<Member>();
            // Check If User Email Already Exist
            var memberPhoto = await memberRepo.GetAllAsync(m => m.Phone == phone && m.Id != memberId);
            return memberPhoto.Any();
        }
        private async Task<bool> IsPhoneExist(string phone) {
            var memberRepo = _unitOfWork.GetRepository<Member>();
            // Check If User Email Already Exist
            var memberPhoto = await memberRepo.GetAllAsync(m => m.Phone == phone);
            return memberPhoto.Any();
        }
        #endregion
    }
}
