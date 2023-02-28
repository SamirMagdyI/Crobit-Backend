using AG.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Emit;

namespace AG
{
    public class AppContext : IdentityDbContext
    {
        public AppContext(DbContextOptions option)
            : base(option)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           
                
        }
        public DbSet<Diseases> Diseases { get; set; }
        public DbSet<Status> statuses { get; set; }
        public DbSet<Photo> photos { get; set; }
        public DbSet<Notification> notifications { get; set; }
        public DbSet<HasDisease> hasDiseases { get; set; }
        public DbSet<Location> location { get; set; }   
        public DbSet<Point> point { get; set; }
    }
}
