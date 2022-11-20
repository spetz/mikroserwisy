using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoHub.Services.Channels.Core.Entities;

namespace VideoHub.Services.Channels.Core.DAL.Configurations;

internal sealed class ChannelSubscriptionConfiguration : IEntityTypeConfiguration<ChannelSubscription>
{
    public void Configure(EntityTypeBuilder<ChannelSubscription> builder)
    {
        builder.HasKey(x => new {x.ChannelId, x.UserId});
    }
}