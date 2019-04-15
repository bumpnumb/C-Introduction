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
        public DbSet<CompetitionUsers> CompetitionUsers { get; set; }
        public DbSet<Results> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Salt).IsRequired();
                entity.Property(e => e.Hash).IsRequired();
                entity.Property(e => e.Cookie).IsRequired();
                entity.Property(e => e.CookieTime).IsRequired();
            });
            modelBuilder.Entity<Competition>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Start).IsRequired();
                entity.Property(e => e.Finished).IsRequired();
            });
            modelBuilder.Entity<CompetitionUsers>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.UID).IsRequired();
                entity.Property(e => e.CID).IsRequired();
            });
            modelBuilder.Entity<Results>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.CID).IsRequired();
                entity.Property(e => e.UID).IsRequired();
                entity.Property(e => e.Result).IsRequired();
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
