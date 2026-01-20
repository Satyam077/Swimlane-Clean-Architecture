using Microsoft.EntityFrameworkCore;
using Swimlane.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swimlane.Infrastructure.Databases
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        public DbSet<Test> Tests { get; set; }
    }
}
