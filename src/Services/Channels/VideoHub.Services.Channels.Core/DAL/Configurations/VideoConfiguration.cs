using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoHub.Services.Channels.Core.Entities;

namespace VideoHub.Services.Channels.Core.DAL.Configurations;

internal sealed class VideoConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();
    }
}