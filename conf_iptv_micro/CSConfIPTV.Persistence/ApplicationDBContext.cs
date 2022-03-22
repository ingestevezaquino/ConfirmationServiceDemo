using System;
using System.Collections.Generic;
using CSConfIPTV.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CSConfIPTV.Persistence
{
    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() { }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options) { }

        public virtual DbSet<Diagnostic> Diagnostics { get; set; } = null!;
        public virtual DbSet<Facility> Facilities { get; set; } = null!;
        public virtual DbSet<Process> Processes { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

            modelBuilder.Entity<Diagnostic>(entity =>
            {
                entity.ToTable("diagnostics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("creationtime")
                    .HasDefaultValueSql(Database.IsNpgsql() ? "now()" : "DATETIME('now')");

                entity.Property(e => e.FacilityId).HasColumnName("facilityid");

                entity.Property(e => e.IsConfigured).HasColumnName("isconfigured");

                entity.Property(e => e.OLTAdminState).HasColumnName("oltadminstate");

                entity.Property(e => e.OLTOperState).HasColumnName("oltoperstate");

                entity.Property(e => e.ONTAdminState).HasColumnName("ontadminstate");

                entity.Property(e => e.ONTOperState).HasColumnName("ontoperstate");

                entity.Property(e => e.ONTRxPower).HasColumnName("ontrxpower");

                entity.Property(e => e.ONTTxPower).HasColumnName("onttxpower");

                entity.Property(e => e.ONTVoltage).HasColumnName("ontvoltage");

                entity.Property(e => e.Username)
                    .HasMaxLength(256)
                    .HasColumnName("username")
                    .HasDefaultValueSql(Database.IsNpgsql() ? "CURRENT_USER" : "'Test_User'");

                entity.HasOne(d => d.Facility)
                    .WithMany(p => p.Diagnostics)
                    .HasForeignKey(d => d.FacilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_diagnostics_facilityid");
            });

            modelBuilder.Entity<Facility>(entity =>
            {
                entity.ToTable("facilities");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("creationtime")
                    .HasDefaultValueSql(Database.IsNpgsql() ? "now()" : "DATETIME('now')");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(20)
                    .HasColumnName("ipaddress");

                entity.Property(e => e.NodeAddress)
                    .HasMaxLength(50)
                    .HasColumnName("nodeaddress");

                entity.Property(e => e.NodeName)
                    .HasMaxLength(50)
                    .HasColumnName("nodename");

                entity.Property(e => e.SubscriberNumber)
                    .HasMaxLength(25)
                    .HasColumnName("subscribernumber");

                entity.Property(e => e.Username)
                    .HasMaxLength(256)
                    .HasColumnName("username")
                    .HasDefaultValueSql(Database.IsNpgsql() ? "CURRENT_USER" : "'Test_User'");

                entity.HasOne(d => d.SubscriberNumberNavigation)
                    .WithMany(p => p.Facilities)
                    .HasPrincipalKey(p => p.SubscriberNumber)
                    .HasForeignKey(d => d.SubscriberNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_facilities_subscribernumber");
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.ToTable("processes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("creationtime")
                    .HasDefaultValueSql(Database.IsNpgsql() ? "now()" : "DATETIME('now')");

                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.IsActive).HasColumnName("isactive");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .HasColumnName("name");

                entity.Property(e => e.Username)
                    .HasMaxLength(256)
                    .HasColumnName("username")
                    .HasDefaultValueSql(Database.IsNpgsql() ? "CURRENT_USER" : "'Test_User'");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("tickets");

                entity.HasIndex(e => e.CaseNumber, "uq_tickets_casenumber")
                    .IsUnique();

                entity.HasIndex(e => e.SubscriberNumber, "uq_tickets_subscribernumber")
                    .IsUnique();

                entity.HasIndex(e => e.UAC, "uq_tickets_uac")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CaseNumber)
                    .HasMaxLength(10)
                    .HasColumnName("casenumber");

                entity.Property(e => e.ClientContactPhone)
                    .HasMaxLength(25)
                    .HasColumnName("clientcontactphone");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(100)
                    .HasColumnName("clientname");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("creationtime")
                    .HasDefaultValueSql(Database.IsNpgsql() ? "now()" : "DATETIME('now')");

                entity.Property(e => e.CurrentQueue)
                    .HasMaxLength(50)
                    .HasColumnName("currentqueue");

                entity.Property(e => e.DestinationQueue)
                    .HasMaxLength(50)
                    .HasColumnName("destinationqueue");

                entity.Property(e => e.ProcessName)
                    .HasMaxLength(25)
                    .HasColumnName("processname");

                entity.Property(e => e.ProductType)
                    .HasMaxLength(25)
                    .HasColumnName("producttype");

                entity.Property(e => e.Status)
                    .HasMaxLength(250)
                    .HasColumnName("status");

                entity.Property(e => e.SubscriberNumber)
                    .HasMaxLength(25)
                    .HasColumnName("subscribernumber");

                entity.Property(e => e.UAC)
                    .HasMaxLength(15)
                    .HasColumnName("uac");

                entity.Property(e => e.Username)
                    .HasMaxLength(256)
                    .HasColumnName("username")
                    .HasDefaultValueSql(Database.IsNpgsql() ? "CURRENT_USER" : "'Test_User'");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
