namespace BLL.DTO
{
    public sealed class FileDTO : IEntityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AccessLevel { get; set; }
        public int FolderId { get; set; }
        public FolderDTO Folder { get; set; }
        public byte[] FileBytes { get; set; }
    }
}