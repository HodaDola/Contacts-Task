using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactNumbers.Models
{
    public class ContactsContext:DbContext
    {
        public ContactsContext(DbContextOptions<ContactsContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contacts>(c =>
            {
                c.HasKey(c => c.Id);
                c.Property(c => c.Name).HasMaxLength(60);
                c.Property(c => c.Name).IsRequired();
                c.Property(c => c.Phone).HasMaxLength(14);
                c.Property(c => c.Phone).IsRequired();
                c.Property(c => c.Address).HasMaxLength(150);
                c.Property(c => c.Notes).HasMaxLength(200);
            });

        }

        public DbSet<Contacts> Contact { get; set; }
    }
}
