using System;
using ECommerce.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.EntityConfigurations.User;

public class UserEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.User>
{
    public override void Configure(EntityTypeBuilder<Api.Domain.Models.User> builder)
    {
        base.Configure(builder);

        builder.ToTable("user", EntityContext.DEFAULT_SCHEMA);
    }
}

