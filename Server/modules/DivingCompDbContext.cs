using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Server.modules
{
    public class DivingCompDbContext : DbContext
    {
        //Classes to be fetched from the db, name is exact. Read up on DBContext for info
        public DbSet<User> Users { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<CompetitionJudge> CompetitionJudges { get; set; }
        public DbSet<CompetitionUser> CompetitionUsers { get; set; }
        public DbSet<Jump> Jumps { get; set; }
        public DbSet<Result> Results { get; set; }


        public static readonly Microsoft.Extensions.Logging.LoggerFactory _myLoggerFactory =
            new LoggerFactory(new[] {
                new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
            });
        
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
                entity.Property(e => e.Jumps).IsRequired();

            });
            modelBuilder.Entity<CompetitionUser>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.UID).IsRequired();
                entity.Property(e => e.CID).IsRequired();
            });
            modelBuilder.Entity<CompetitionJudge>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.UID).IsRequired();
                entity.Property(e => e.CID).IsRequired();
            });
            modelBuilder.Entity<Jump>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Code).IsRequired();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Difficulty).IsRequired();
                entity.Property(e => e.CUID).IsRequired();
                entity.Property(e => e.Number).IsRequired();
                entity.Property(e => e.GlobalNumber).IsRequired();
                entity.Property(e => e.Height).IsRequired();
                entity.HasMany(e => e.Results);
            });
            modelBuilder.Entity<Result>(entity =>
            {
                entity.HasKey(e => e.JumpID);
                entity.Property(e => e.Score).IsRequired();
                entity.HasKey(e => e.JudgeID);
                entity.HasOne(e => e.Jump).WithMany(j => j.Results);
            });


            //modelBuilder.Entity<Result>().HasRequired(e => e.OldStep).WithMany().HasForeignKey(e => e.OldStepId);


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseLoggerFactory(_myLoggerFactory);
            //db info is in gitignore file.
            //fetch file and read connectionstring.
            config c = new config();
            string fp = "..\\..\\..\\..\\.\\config.json";
            optionsBuilder.UseMySQL(c.Read(fp));
        }
    }
}
