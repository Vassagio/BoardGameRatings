﻿using System;
using BoardGameRatings.WebSite.Models.ModelBuilders;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace BoardGameRatings.WebSite.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(IServiceProvider serviceProvider, DbContextOptions options)
            : base(serviceProvider, options)
        {
        }

        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new GameModelBuilder(builder.Entity<Game>()).Build();
        }
    }
}