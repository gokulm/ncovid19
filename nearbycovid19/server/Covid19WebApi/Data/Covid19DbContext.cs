using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoltOn.Data.EF;
using Covid19WebApi.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Covid19WebApi.Data
{
    public class Covid19DbContext : BaseDbContext<Covid19DbContext>
    {
        public Covid19DbContext(DbContextOptions<Covid19DbContext> options) : base(options)
        {
        }

        protected override void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VictimVisitedLocationMapping());
        }
    }
}
