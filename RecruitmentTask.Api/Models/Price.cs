using CsvHelper.Configuration.Attributes;

namespace RecruitmentTask.Api.Models
{
    public class Price
    {
        [Index(0)]
        public string InternalId { get; set; }

        [Index(1)]
        public string Sku { get; set; }

        [Index(2)]
        public decimal? NetPrice { get; set; }

        [Index(3)]
        public decimal? NetDiscountPrice { get; set; }

        [Index(4)]
        [Ignore]
        public string? VatRate { get; set; }

        [Index(5)]
        public decimal? NetDiscountPricePerUnit { get; set; }
    }
}
