﻿using System;
using System.Runtime.Serialization;

namespace ECommerce.Infrastructure.Persistence.Exeptions
{
	public class DatabaseValidationException : Exception
	{
		public DatabaseValidationException()
		{
		}

        public DatabaseValidationException(string? message) : base(message)
        {
        }

        public DatabaseValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DatabaseValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
