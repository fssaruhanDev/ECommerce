using ECommerce.Api.Domain.Models;
using ECommerce.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Persistence.EntityConfigurations.Product;

public class BasketEntityConfiguration : BaseEntityConfiguration<Basket>
{
    public override void Configure(EntityTypeBuilder<Basket> builder)
    {
        base.Configure(builder);

        builder.ToTable("basket", EntityContext.DEFAULT_SCHEMA);

        builder.HasOne(User => User.User)
              .WithMany(i => i.Baskets)
              .HasForeignKey(User => User.UserID);

    }
}
