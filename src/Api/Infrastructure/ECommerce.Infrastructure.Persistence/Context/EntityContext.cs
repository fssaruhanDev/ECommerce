using System;
using System.Reflection;
using ECommerce.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ECommerce.Infrastructure.Persistence.Context
{
	public class EntityContext : DbContext
	{
		public const string DEFAULT_SCHEMA = "dbo";

		public EntityContext()
		{

		}

		public EntityContext(DbContextOptions options) : base(options)
		{
		}



		public DbSet<User> Users { get; set; }
        public DbSet<Basket> Baskets{ get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				var connectionString = "Server=localhost;Database=ECommerceDB;User=fssaruhan;Password=123456Asd;TrustServerCertificate=True;Pooling=true; ";

				optionsBuilder.UseSqlServer(connectionString, x =>
				{
					x.EnableRetryOnFailure();
				});
			}
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}


		public override int SaveChanges()
		{
			OnBeforeSave();
			return base.SaveChanges();
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			OnBeforeSave();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			OnBeforeSave();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			OnBeforeSave();
			return base.SaveChangesAsync(cancellationToken);
		}

		public void OnBeforeSave()
		{
			var addedEntities = ChangeTracker.Entries()
											 .Where(x => x.State == EntityState.Added)
											 .Select(x => (BaseEntity)x.Entity);

			PreperedAddedEntities(addedEntities);

		}

		public void PreperedAddedEntities(IEnumerable<BaseEntity> entities)
		{

			foreach (var entity in entities)
			{
				if(entity.CreateDate == DateTime.MinValue)
					entity.CreateDate = DateTime.Now;

			}

		}

	}
}

