using System.Collections.Generic;

namespace PL.Models
{
    public class FolderView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public List<FolderView> Folders { get; set; }
        public List<FileView> Files { get; set; }

        public int? ParentFolderId { get; set; }

    }
}