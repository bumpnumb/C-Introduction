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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Salt).IsRequired();
                entity.Property(e => e.Hash).IsRequired();
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
