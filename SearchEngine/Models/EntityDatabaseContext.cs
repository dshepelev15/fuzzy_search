using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SearchEngine.Models
{
    public class EntityDatabaseContext : DbContext
    {
        public EntityDatabaseContext(string connectionString)
        {
            Database.Connection.ConnectionString = connectionString;
        }

        public DbSet<Record> Records { get; set; }
    }
}