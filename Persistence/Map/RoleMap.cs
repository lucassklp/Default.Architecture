using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Persistence.Map
{
    class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("role");
            
            builder.Property(x => x.ID).HasColumnName("RoleID");
            builder.Property(x => x.Description).HasColumnName("Description");

            builder.HasKey(x => x.ID);
        }
    }
}
