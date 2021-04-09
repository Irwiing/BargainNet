using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BargainNet.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BargainNet.WebApp.Areas.Identity.Pages.Data
{
    public class BargainContext : IdentityDbContext<User>
    {
        public BargainContext(DbContextOptions<BargainContext> options)
            : base(options)
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
    }
}
