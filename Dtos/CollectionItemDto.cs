namespace MotorinApi.Dtos
{
    public partial class CreateCollectionItemDto
    {
        public System.Guid CollectionId { get; set; }
        public System.Guid CatalogId { get; set; }
        public string Condition { get; set; } = string.Empty;
        public bool IsInPackage { get; set; }
        public string PackageCondition { get; set; } = string.Empty;
        public DateTime AcquisitionDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal EstimatedValue { get; set; }
        public string UserNotes { get; set; } = string.Empty;
        public string UserImageUrl { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}