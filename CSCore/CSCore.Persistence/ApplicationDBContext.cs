using System;
using System.Collections.Generic;
using CSCore.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CSCore.Persistence
{
    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() {}

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options) {}

        public virtual DbSet<Diagnostic> Diagnostics { get; set; } = null!;
        public virtual DbSet<Facility> Facilities { get; set; } = null!;
        public virtual DbSet<Process> Processes { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

            modelBuilder.Entity<Diagnostic>(entity =>
            {
                entity.ToTable("diagnostics");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.TicketId)
                    .HasColumnName("ticketid");

                entity.Property(e => e.IsConfigured)
                    .HasColumnName("isconfigured");

                entity.Property(e => e.OLTAdminState)
                    .HasColumnName("oltadminstate");

                entity.Property(e => e.OLTOperState)
                    .HasColumnName("oltoperstate");

                entity.Property(e => e.ONTAdminState)
                    .HasColumnName("ontadminstate");

                entity.Property(e => e.ONTOperState)
                    .HasColumnName("ontoperstate");

                entity.Property(e => e.ONTRxPower)
                    .HasColumnName("ontrxpower");

                entity.Property(e => e.ONTTxPower)
                    .HasColumnName("onttxpower");

                entity.Property(e => e.ONTVoltage)
                    .HasColumnName("ontvoltage");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creationtime")
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql(Database.IsNpgsql() ? "now()" : "DATETIME('now')");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(256)
                    .HasDefaultValueSql(Database.IsNpgsql() ? "CURRENT_USER" : "'Test_User'");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Diagnostics)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_diagnostics_ticketid");
            });

            modelBuilder.Entity<Facility>(entity =>
            {
                entity.ToTable("facilities");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.TicketId)
                    .HasColumnName("ticketid");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creationtime")
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql(Database.IsNpgsql() ? "now()" : "DATETIME('now')");

                entity.Property(e => e.IpAddress)
                    .HasColumnName("ipaddress")
                    .HasMaxLength(20);

                entity.Property(e => e.NodeAddress)
                    .HasColumnName("nodeaddress")
                    .HasMaxLength(50);

                entity.Property(e => e.NodeName)
                    .HasColumnName("nodename")
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(256)
                    .HasDefaultValueSql(Database.IsNpgsql() ? "CURRENT_USER" : "'Test_User'");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Facilities)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_facilities_ticketid");
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.ToTable("processes");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creationtime")
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql(Database.IsNpgsql() ? "now()" : "DATETIME('now')");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(25);

                entity.Property(e => e.IsActive)
                    .HasColumnName("isactive");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(256)
                    .HasDefaultValueSql(Database.IsNpgsql() ? "CURRENT_USER" : "'Test_User'");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("tickets");

                entity.HasIndex(e => e.CaseNumber, "uq_tickets_casenumber")
                    .IsUnique();

                entity.HasIndex(e => e.UAC, "uq_tickets_uac")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.CaseNumber)
                    .HasColumnName("casenumber")    
                    .HasMaxLength(10);

                entity.Property(e => e.ClientContactPhone)
                    .HasColumnName("clientcontactphone")    
                    .HasMaxLength(25);

                entity.Property(e => e.ClientName)
                    .HasColumnName("clientname")
                    .HasMaxLength(100);

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creationtime")
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql(Database.IsNpgsql() ? "now()" : "DATETIME('now')");

                entity.Property(e => e.CurrentQueue)
                    .HasColumnName("currentqueue")
                    .HasMaxLength(50);

                entity.Property(e => e.DestinationQueue)
                    .HasColumnName("destinationqueue")
                    .HasMaxLength(50);

                entity.Property(e => e.ProcessName)
                    .HasColumnName("processname")
                    .HasMaxLength(25);

                entity.Property(e => e.ProductType)
                    .HasColumnName("producttype")
                    .HasMaxLength(25);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(250);

                entity.Property(e => e.SubscriberNumber)
                    .HasColumnName("subscribernumber")
                    .HasMaxLength(25);

                entity.Property(e => e.UAC)
                    .HasColumnName("uac")
                    .HasMaxLength(15);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(256)
                    .HasDefaultValueSql(Database.IsNpgsql() ? "CURRENT_USER" : "'Test_User'");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
