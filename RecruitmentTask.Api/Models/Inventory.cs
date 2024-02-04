using CsvHelper.Configuration.Attributes;

namespace RecruitmentTask.Api.Models
{
    public class Inventory
    {
        [Name("product_id")]
        public int Id { get; set; }

        [Name("sku")]
        public string Sku { get; set; }

        [Name("unit")]
        public string? Unit { get; set; }

        [Name("qty")]
        public decimal? Quantity { get; set; }

        [Name("manufacturer_name")]
        public string? Manufacturer { get; set; }

        [Name("shipping")]
        public string? ShippingTime { get; set; }

        [Name("shipping_cost")]
        public decimal? ShippingCost { get; set; }
    }
}
