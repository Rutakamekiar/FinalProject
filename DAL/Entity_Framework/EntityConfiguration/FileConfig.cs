using System.Data.Entity.ModelConfiguration;
using DAL.Entities;

namespace DAL.Entity_Framework.EntityConfiguration
{
    public sealed class FileConfig : EntityTypeConfiguration<File>
    {
        public FileConfig()
        {
            Property(f => f.Name).IsRequired();
            Property(f => f.AccessLevel).IsRequired();
            Property(f => f.Path).IsRequired();
            Property(f => f.UserId).IsRequired();
        }
    }
}
