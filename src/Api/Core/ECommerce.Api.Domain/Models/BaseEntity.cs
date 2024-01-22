using System;
namespace ECommerce.Api.Domain.Models
{
	public abstract class BaseEntity
	{
		public Guid ID { get; set; }
		public DateTime CreateDate { get; set; }
    }
}

