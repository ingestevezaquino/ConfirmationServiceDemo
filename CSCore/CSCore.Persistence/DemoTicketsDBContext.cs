using System;
using System.Collections.Generic;
using CSCore.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CSCore.Persistence
{
    public partial class DemoTicketsDBContext : DbContext
    {
        public DemoTicketsDBContext() {}

        public DemoTicketsDBContext(DbContextOptions<DemoTicketsDBContext> options)
            : base(options) {}

        public virtual DbSet<Diagnostic> Diagnostics { get; set; } = null!;
        public virtual DbSet<Facility> Facilities { get; set; } = null!;
        public virtual DbSet<Process> Processes { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diagnostic>(entity =>
            {
                entity.Property(e => e.CreationTime).HasDefaultValueSql(Database.IsSqlServer() ? "(getdate())" : "DATETIME('now')");

                entity.Property(e => e.HostName)
                    .HasMaxLength(128)
                    .HasDefaultValueSql(Database.IsSqlServer() ? "(host_name())" : "'Test_Host'");

                entity.Property(e => e.UserName)
                    .HasMaxLength(256)
                    .HasDefaultValueSql(Database.IsSqlServer() ? "(suser_name())" : "'Test_User'");
            });

            modelBuilder.Entity<Facility>(entity =>
            {
                entity.Property(e => e.CreationTime).HasDefaultValueSql(Database.IsSqlServer() ? "(getdate())" : "DATETIME('now')");

                entity.Property(e => e.HostName)
                    .HasMaxLength(128)
                    .HasDefaultValueSql(Database.IsSqlServer() ? "(host_name())" : "'Test_Host'");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NodeAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NodeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(256)
                    .HasDefaultValueSql(Database.IsSqlServer() ? "(suser_name())" : "'Test_User'");
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.Property(e => e.CreationTime).HasDefaultValueSql(Database.IsSqlServer() ? "(getdate())" : "DATETIME('now')");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.HostName)
                    .HasMaxLength(128)
                    .HasDefaultValueSql(Database.IsSqlServer() ? "(host_name())" : "'Test_Host'");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(256)
                    .HasDefaultValueSql(Database.IsSqlServer() ? "(suser_name())" : "'Test_User'");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasIndex(e => e.CaseNumber, "UQ_TICKETS_CASENUMBER")
                    .IsUnique();

                entity.HasIndex(e => e.UAC, "UQ_TICKETS_UAC")
                    .IsUnique();

                entity.Property(e => e.CaseNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ClientContactPhone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ClientName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreationTime).HasDefaultValueSql(Database.IsSqlServer() ? "(getdate())" : "DATETIME('now')");

                entity.Property(e => e.CurrentQueue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationQueue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HostName)
                    .HasMaxLength(128)
                    .HasDefaultValueSql(Database.IsSqlServer() ? "(host_name())" : "'Test_Host'");

                entity.Property(e => e.ProcessName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ProductType)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SubscriberNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UAC)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(256)
                    .HasDefaultValueSql(Database.IsSqlServer() ? "(suser_name())" : "'Test_User'");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
