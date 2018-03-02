using DateLayer.Models;
using EFCoreGenericRepository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DateLayer.Repositories
{
	public class ManagerRepository : GenericRepository<Manager>
	{		public ManagerRepository(DbContext context) : base(context) { }
	}
}
