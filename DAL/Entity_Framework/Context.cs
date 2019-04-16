using System.Data.Entity;
using DAL.Entities;
using DAL.Entity_Framework.Conventions;
using DAL.Entity_Framework.EntityConfiguration;

namespace DAL.Entity_Framework
{
    public sealed class Context : DbContext
    {
        public DbSet<File> Files { get; set; }

        static Context()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public Context() : base("Entity")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FileConfig());
            modelBuilder.Conventions.Add(new StringConvention());
        }
    }
}