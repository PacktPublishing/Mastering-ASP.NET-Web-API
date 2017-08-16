namespace Puhoi.DataModels
{
    public class StoreDto : BaseDto
    {
        public long StoreId { get; set; }
        public int NumberOfProducts { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}
