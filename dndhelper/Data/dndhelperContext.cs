using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dndhelper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Configuration;

namespace dndhelper.Data
{
    public class dndhelperContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<Microsoft.AspNetCore.Identity.IdentityUser, Microsoft.AspNetCore.Identity.IdentityRole, string>
    {
        public dndhelperContext (DbContextOptions<dndhelperContext> options)
            : base(options)
        {
        }

        public DbSet<dndhelper.Models.DiceModel> DiceModel { get; set; } = default!;

        public DbSet<dndhelper.Models.CharacterGenerator>? CharacterGenerator { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ...

            modelBuilder.Entity<CharacterGenerator>()
                .Property(c => c.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            // ...

            base.OnModelCreating(modelBuilder);
        }
    }
}
