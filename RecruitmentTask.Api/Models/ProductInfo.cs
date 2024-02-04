namespace RecruitmentTask.Api.Models
{
    public class ProductInfo
    {
        public string? Name { get; set; }
        public string? Ean { get; set; }
        public string? ProducerName { get; set; }
        public string? Category { get; set; }
        public string? DefaultImage { get; set; }
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal? NetDiscountPricePerUnit { get; set; }
        public decimal? ShippingCost { get; set; }
    }
}
