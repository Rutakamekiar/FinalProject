namespace DAL.Entities
{
    public class File : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AccessLevel { get; set; }
        public int FolderId { get; set; }
        public virtual Folder Folder { get; set; }
    }
}
