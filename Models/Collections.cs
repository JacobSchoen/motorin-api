namespace MotorinApi
{
    public partial class Collections
    {
        public System.Guid CollectionId { get; set; }
        public System.Guid UserId { get; set; }
        public string CollectionType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }

        public Collections()
        {
            if (CollectionType == null)
            {
                CollectionType = "";
            }
            if (Name == null)
            {
                Name = "";
            }
            if (Description == null)
            {
                Description = "";
            }
        }
    }
}