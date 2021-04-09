﻿using BargainNet.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Infra.SQL.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>();
            builder.Entity<UserProfile>()
                .HasDiscriminator<string>("type")
                .HasValue<LegalPerson>("legal_person")
                .HasValue<NaturalPerson>("natural_person");
            base.OnModelCreating(builder);

        }
        public DbSet<User> UserPerson { get; set; }
        public DbSet<NaturalPerson> NaturalPersons { get; set; }
        public DbSet<LegalPerson> LegalPersons { get; set; }
    }
}
