﻿using System;
using ECommerce.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(i => i.ID);

        builder.Property(i => i.ID).ValueGeneratedOnAdd();

        builder.Property(i => i.CreateDate).ValueGeneratedOnAdd();
    }
}