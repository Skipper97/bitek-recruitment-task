using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;

namespace RecruitmentTask.Api.Models
{
    public class Product
    {
        [Name("ID")]
        public int Id { get; set; }

        [Name("SKU")]
        public string Sku { get; set; }

        [Name("name")]
        public string? Name { get; set; }

        [Name("EAN")]
        public string? Ean { get; set; }

        [Name("producer_name")]
        public string? ProducerName { get; set; }

        [Name("category")]
        public string? Category { get; set; }

        [Name("is_wire")]
        public int IsWire { get; set; }

        [Name("available")]
        public int Available { get; set; }

        [Name("is_vendor")]
        public int IsVendor { get; set; }

        [Name("default_image")]
        public string? DefaultImage { get; set; }
    }
}
