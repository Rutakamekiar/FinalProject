using System.Data.Entity;
using DAL.Entities;

namespace DAL.Entity_Framework
{
    public sealed class DbInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context db)
        {
            
        }
    }
}