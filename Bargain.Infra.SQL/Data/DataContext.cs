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
            builder.Entity<User>().HasOne(u => u.UserProfile);
            builder.Entity<User>().HasOne(u => u.UserProfile);
            builder.Entity<Category>().HasMany(c => c.UserProfiles);
            builder.Entity<UserProfile>().HasOne(up => up.LegalPerson);
            builder.Entity<UserProfile>().HasOne(up => up.NaturalPerson);
            builder.Entity<UserProfile>().HasMany(up => up.Interests);
            builder.Entity<UserProfile>().HasOne(up => up.Address);
            builder.Entity<UserProfile>().HasMany(up => up.AdAuctions);
            builder.Entity<AdAuction>().HasOne(aa => aa.Category);
            builder.Entity<AdAuction>().HasOne(aa => aa.AdAcutionSettings);
            builder.Entity<AdAuction>().HasMany(aa => aa.Offers);
            builder.Entity<Offer>().HasOne(o => o.User);
            builder.Entity<Chat>().HasMany(c => c.Messages);
            builder.Entity<Chat>().HasOne(c => c.AuctionOwner);
            builder.Entity<Chat>().HasOne(c => c.AuctionWinner);
            builder.Entity<Chat>().HasOne(c => c.Auction);
            base.OnModelCreating(builder);

        }
        public DbSet<User> UserPerson { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AdAuction> Auctions { get; set; }
        public DbSet<Offer> AllOffers { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> Messages{ get; set; }
        public DbSet<AdAcutionSettings> AdAcutionSettings { get; set; }
    }
}

