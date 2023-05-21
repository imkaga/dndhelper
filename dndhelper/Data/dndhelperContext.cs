using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dndhelper.Models;

namespace dndhelper.Data
{
    public class dndhelperContext : DbContext
    {
        public dndhelperContext (DbContextOptions<dndhelperContext> options)
            : base(options)
        {
        }

        public DbSet<dndhelper.Models.DiceModel> DiceModel { get; set; } = default!;
    }
}
