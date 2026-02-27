namespace MotorinApi.Dtos
{
    public partial class CreateCollectionDto
    {
        public System.Guid UserId { get; set; }
        public string CollectionType { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}