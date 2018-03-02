using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DateLayer.Models
{
	public partial class KyivgazDBContext : DbContext
	{
		public KyivgazDBContext(DbContextOptions options) : base(options) { }

		public virtual DbSet<Invoice> Invoice { get; set; }
		public virtual DbSet<Manager> Manager { get; set; }

		
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//            if (!optionsBuilder.IsConfigured)
			//            {
			//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
			//                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=KyivgazDB;Trusted_Connection=True;");
			//            }
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Invoice>(entity =>
			{
				entity.Property(e => e.Annotation).HasMaxLength(512);

				entity.Property(e => e.DateCreated).HasColumnType("datetime");

				entity.Property(e => e.DateOfShipment).HasColumnType("datetime");

				entity.Property(e => e.InvoiceNumber)
					.IsRequired()
					.HasMaxLength(50);

				entity.HasOne(d => d.Manager)
					.WithMany(p => p.Invoice)
					.HasForeignKey(d => d.ManagerId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Invoice__Manager__25869641");
			});

			modelBuilder.Entity<Manager>(entity =>
			{
				entity.Property(e => e.LastName)
					.IsRequired()
					.HasMaxLength(64);
			});
		}
	}
}
