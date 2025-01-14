using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CardsServer.DAL.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Email).IsUnique();

            // Один пользователь может иметь много созданных модулей
            builder.HasMany(x => x.CreatedModules)
                .WithOne(x => x.Creator)
                .HasForeignKey(x => x.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(x => x.UsedModules)
                .WithMany(x => x.UsedUsers)
                .UsingEntity<UserModule>(
                    j => j
                        .HasOne(pt => pt.Module)
                        .WithMany(t => t.UserModules)
                        .HasForeignKey(pt => pt.ModuleId),
                    j => j
                        .HasOne(x => x.User)
                        .WithMany(x => x.UserModules)
                        .HasForeignKey(x => x.UserId),
                    j =>
                    {
                        j.HasKey(x => new { x.ModuleId, x.UserId });
                        j.ToTable("UserModules");
                    });

            builder.HasMany(x => x.RefreshTokens)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Profile)
                .WithOne(x => x.User)
                .HasForeignKey<ProfileEntity>(x => x.UserId);
        }
    }
}
