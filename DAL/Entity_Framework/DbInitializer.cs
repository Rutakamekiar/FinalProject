using System.Data.Entity;
using DAL.Entities;

namespace DAL.Entity_Framework
{
    public sealed class DbInitializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context db)
        {
            var testFile = new File() {Name = "hello.txt", AccessLevel = 1, Path = "path", UserId = 1.ToString()};
            db.Files.Add(testFile);
            db.SaveChanges();
        }
    }
}