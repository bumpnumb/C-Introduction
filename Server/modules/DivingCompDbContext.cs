using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;


namespace Server.modules
{
    public class DivingCompDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<CompetitionUser> CompetitionUsers { get; set; }
        public DbSet<UICJump> UICJumps { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.SSN).IsRequired();
                entity.Property(e => e.Group).IsRequired();
                entity.Property(e => e.Salt).IsRequired();
                entity.Property(e => e.Hash).IsRequired();
            });
            modelBuilder.Entity<Competition>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Start).IsRequired();
                entity.Property(e => e.Finished).IsRequired();
            });
            modelBuilder.Entity<CompetitionUser>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.UID).IsRequired();
                entity.Property(e => e.CID).IsRequired();
            });
            modelBuilder.Entity<UICJump>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.UICID).IsRequired();
                entity.Property(e => e.Code).IsRequired();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Difficulty).IsRequired();
            });
            modelBuilder.Entity<Result>(entity =>
            {
                entity.Property(e => e.UICJID).IsRequired();
                entity.Property(e => e.Score).IsRequired();
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            config c = new config();
            string fp = "..\\..\\..\\..\\.\\config.json";
            optionsBuilder.UseMySQL(c.Read(fp));
        }
    }
}
