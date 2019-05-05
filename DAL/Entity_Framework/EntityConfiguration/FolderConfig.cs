using DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DAL.Entity_Framework.EntityConfiguration
{
    public class FolderConfig : EntityTypeConfiguration<Folder>
    {
        public FolderConfig()
        {
            //Property(f => f.UserId).IsRequired();
            //Property(f => f.Path).IsRequired();
            Property(f => f.Name).IsRequired();
            HasMany(f => f.Files)
                .WithRequired(f => f.Folder)
                .HasForeignKey(f => f.FolderId)
                .WillCascadeOnDelete(true);
            HasMany(f => f.Folders)
                .WithOptional(f => f.ParentFolder)
                .HasForeignKey(f => f.ParentFolderId)
                ;//.WillCascadeOnDelete(true);
            HasOptional(f => f.ParentFolder).WithMany(f => f.Folders);//.WillCascadeOnDelete(true);
        }
    }
}
