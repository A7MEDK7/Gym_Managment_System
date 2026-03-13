using Domin.GymEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configurations {
    public class MemberSessionConfiguration : IEntityTypeConfiguration<MemberSession> {
        public void Configure(EntityTypeBuilder<MemberSession> builder) {
            builder.Ignore(X => X.Id);
            builder.HasKey(X => new { X.MemberId, X.SessionId });
            builder.Property(X => X.CreatedAt)
                .HasColumnName("BookingDate")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
