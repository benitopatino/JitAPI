﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using JitAPI.Models.Follows;

namespace JitAPI.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Jit> Jits { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Login> Logins { get; set; }

        public DbSet<UserFollow> UserFollows { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserFollow>()
                .HasIndex(f => new { f.UserFollowerId, f.UserFolloweeId })
                .IsUnique();
        }
    }
}
