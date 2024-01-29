using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Domain.Entities;

namespace Notifications.Infrastructure.Persistence.Configurations;

public class NotificationTypeUserConfiguration : IEntityTypeConfiguration<NotificationTypeUser>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<NotificationTypeUser> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Id).ValueGeneratedOnAdd();

        builder.Property(n => n.Channel).IsRequired();

        builder.Property(n => n.Channel).IsRequired();

        builder.Property(n => n.WorkspaceId).IsRequired(false);
    }
}