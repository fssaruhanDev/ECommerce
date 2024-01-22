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

public class OrderEntityConfiguration : BaseEntityConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder.ToTable("order", EntityContext.DEFAULT_SCHEMA);

        builder.HasOne(i => i.Basket)
               .WithMany(i => i.Orders)
               .HasForeignKey(i => i.BasketID);

        builder.HasOne(i => i.User)
               .WithMany(i => i.Orders)
               .HasForeignKey(i => i.UserID);

    }
}
