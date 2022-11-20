using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoHub.Services.Users.Core.Entities;

namespace VideoHub.Services.Users.Core.DAL.Configurations;

internal class UserSubscriptionConfiguration : IEntityTypeConfiguration<UserSubscription>
{
    public void Configure(EntityTypeBuilder<UserSubscription> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.HasIndex(x => x.UserId).IsUnique();
        builder.Property(x => x.Version).IsConcurrencyToken();
    }
}