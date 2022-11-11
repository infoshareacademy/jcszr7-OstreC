using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fangehull.Models;

namespace Fangehull.Data
{
    public class FangehullContext : DbContext
    {
        public FangehullContext (DbContextOptions<FangehullContext> options)
            : base(options)
        {
        }

        public DbSet<Fangehull.Models.Monster> Monster { get; set; } = default!;
    }
}
