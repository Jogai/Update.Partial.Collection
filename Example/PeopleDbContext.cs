using System.Data.Common;
using System.Data.Entity;
using Example.Migrations;
using Example.Models;

namespace Example
{
    public class PeopleDbContext : DbContext
    {
        private DbConnection dbConnection;

        public PeopleDbContext()
            : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
            Database.SetInitializer<PeopleDbContext>(new MigrateDatabaseToLatestVersion<PeopleDbContext, Configuration>());
        }

        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<Child> Children { get; set; }
        public virtual DbSet<Schema> Schemata { get; set; }
        public virtual DbSet<ChildSchema> ChildSchemata { get; set; }
    }
}