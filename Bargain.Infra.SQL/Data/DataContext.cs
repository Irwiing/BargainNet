using BargainNet.Core.Entities;
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
            builder.Entity<User>().HasOne(u => u.LegalPerson);
            builder.Entity<User>().HasOne(u => u.NaturalPerson);
            builder.Entity<UserProfile>()
                .HasDiscriminator<string>("type")
                .HasValue<LegalPerson>("legal_person")
                .HasValue<NaturalPerson>("natural_person");
            builder.Entity<UserProfile>().HasOne(up => up.Address);

            base.OnModelCreating(builder);

        }
        public DbSet<User> UserPerson { get; set; }
        public DbSet<NaturalPerson> NaturalPersons { get; set; }
        public DbSet<LegalPerson> LegalPersons { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
