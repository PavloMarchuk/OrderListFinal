using DateLayer.Models;
using EFCoreGenericRepository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DateLayer.Repositories
{
	public class InvoiceRepository : GenericRepository<Invoice>
	{
		public InvoiceRepository(DbContext context) : base(context) { }
	}
}
