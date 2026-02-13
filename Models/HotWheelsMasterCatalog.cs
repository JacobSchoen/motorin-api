namespace MotorinApi.Models
{
    public partial class HotWheelsMasterCatalog
    {
        public System.Guid CatalogId { get; set; }
        public string ProductLine { get; set; }
        public string ModelName { get; set; }
        public string ToyNumber { get; set; }
        public string SeriesName { get; set; }
        public string SeriesNumber { get; set; }
        public string CastingName { get; set; }
        public string ColorVariant { get; set; }
        public string TampoDesign { get; set; }
        public bool TreasureHunt { get; set; }
        public bool SuperTreasureHunt { get; set; }
        public int ManufactureYear { get; set; }
        public string ProductNumber { get; set; }
        public string OfficialImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public HotWheelsMasterCatalog()
        {
            if (ProductLine == null)
            {
                ProductLine = "";
            }
            if (ModelName == null)
            {
                ModelName = "";
            }
            if (ToyNumber == null)
            {
                ToyNumber = "";
            }
            if (SeriesName == null)
            {
                SeriesName = "";
            }
            if (SeriesNumber == null)
            {
                SeriesNumber = "";
            }
            if (CastingName == null)
            {
                CastingName = "";
            }
            if (ColorVariant == null)
            {
                ColorVariant = "";
            }
            if (TampoDesign == null)
            {
                TampoDesign = "";
            }
            if (ProductNumber == null)
            {
                ProductNumber = "";
            }
            if (OfficialImageUrl == null)
            {
                OfficialImageUrl = "";
            }
            if (Description == null)
            {
                Description = "";
            }
        }
    }
}