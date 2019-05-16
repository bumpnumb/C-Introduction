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
        //Classes to be fetched from the db, name is exact. Read up on DBContext for info
        public DbSet<User> Users { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<CompetitionJudge> CompetitionJudges { get; set; }
        public DbSet<CompetitionUser> CompetitionUsers { get; set; }
        public DbSet<UICJump> UICJumps { get; set; }
        public DbSet<Result> Results { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Override OnModelCreating to fetch our data from db.
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
            modelBuilder.Entity<CompetitionJudge>(entity =>
            {
                entity.HasKey(e => e.UID);
                entity.HasKey(e => e.CID);
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
                entity.HasKey(e => e.UICJID);
                entity.HasKey(e => e.Score);
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //db info is in gitignore file.
            //fetch file and read connectionstring.
            config c = new config();
            string fp = "..\\..\\..\\..\\.\\config.json";
            optionsBuilder.UseMySQL(c.Read(fp));
        }
    }
}
