using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Persistence.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.Property(x => x.ID).HasColumnName("UserID");
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.Password).HasColumnName("Password");
            builder.Property(x => x.Email).HasColumnName("Email");

            builder.HasKey(x => x.ID);
        }
    }
}
