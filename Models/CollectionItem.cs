namespace MotorinApi
{
    public partial class CollectionItem
    {
        public System.Guid CollectionId { get; set; }
        public System.Guid UserId { get; set; }
        public System.Guid CatalogId { get; set; }
        public string Condition { get; set; }
        public bool IsInPackage { get; set; }
        public string PackageCondition { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal EstimatedValue { get; set; }
        public string UserNotes { get; set; }
        public string UserImageUrl { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }

        public CollectionItem()
        {
            if (Condition == null)
            {
                Condition = "";
            }
            if (PackageCondition == null)
            {
                PackageCondition = "";
            }
            if (UserNotes == null)
            {
                UserNotes = "";
            }
            if (UserImageUrl == null)
            {
                UserImageUrl = "";
            }
        }
    }
}