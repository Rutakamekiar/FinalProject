using System.Collections.Generic;

namespace DAL.Entities
{
    public class Folder : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Path { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Folder> Folders { get; set; }
        public int? ParentFolderId { get; set; }
        public virtual Folder ParentFolder { get; set; }
        public Folder()
        {
            Files = new List<File>();
            Folders = new List<Folder>();
        }
    }
}
