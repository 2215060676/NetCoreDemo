using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Model
{
    public class DemoContext:IdentityDbContext
    {

        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {
        }

        public DbSet<Uers> Uers { get; set; }

        public DbSet<Result> Results { get; set; }

        public DbSet<ResultType> ResultType { get; set; }
    }
}
