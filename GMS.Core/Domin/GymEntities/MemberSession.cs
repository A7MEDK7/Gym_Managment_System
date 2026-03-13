using Domin.Entities;

namespace Domin.GymEntities {
    public class MemberSession : BaseEntity {
        // BookingDate => CreatedAt Of BaseEntity
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;
    }
}