using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UploadTransaction.Models.DB
{
    public partial class InvoiceContext : DbContext
    {
        public InvoiceContext()
        {
        }

        public InvoiceContext(DbContextOptions<InvoiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TransactionUpload> TransactionUpload { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<TransactionUpload>(entity =>
            //{
            //    entity.Property(e => e.Amount).HasColumnType("numeric(18, 0)");

            //    entity.Property(e => e.Currency)
            //        .HasMaxLength(3)
            //        .IsFixedLength();

            //    entity.Property(e => e.Status)
            //        .HasMaxLength(10)
            //        .IsFixedLength();

            //    entity.Property(e => e.TransactionId).HasMaxLength(50);
            //});

            //OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
