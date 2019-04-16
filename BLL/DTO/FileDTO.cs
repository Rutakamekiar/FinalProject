namespace BLL.DTO
{
    public sealed class FileDTO : IEntityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int AccessLevel { get; set; }
        public string UserId { get; set; }
    }
}