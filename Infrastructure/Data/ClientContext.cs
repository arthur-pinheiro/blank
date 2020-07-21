using ApplicationCore.Entities;
using ApplicationCore.Entities.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions<ClientContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        // Tables
        public DbSet<Client> Clients { get; set; }

        public DbSet<Contact> Contacts { get; set; }
        
        
        // Views
        public DbSet<VP_Client> VP_Clients { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: uncomment when changing model's table name
            //modelBuilder
            //    .Entity<Client>()
            //    .ToTable("db_clients");

            modelBuilder
                .Entity<VP_Client>()
                .HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
