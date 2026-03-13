using Domin.GymEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configurations {
    public class MemberShipConfiguration : IEntityTypeConfiguration<MemberShip> {
        public void Configure(EntityTypeBuilder<MemberShip> builder) {
            builder.Property(X => X.CreatedAt)
                .HasColumnName("StartDate")
                .HasDefaultValueSql("GETDATE()");

            builder.HasKey(X => new {X.MemberId, X.PlanId, X.CreatedAt});
            builder.Ignore(X => X.Id);
            builder.Ignore(X => X.Status);
        }
    }
}
