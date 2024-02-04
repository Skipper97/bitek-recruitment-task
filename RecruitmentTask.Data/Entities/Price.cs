using System;
using System.Collections.Generic;

namespace RecruitmentTask.Data.Entities;

public partial class Price
{
    public string InternalId { get; set; } = null!;

    public string Sku { get; set; } = null!;

    public decimal? NetPrice { get; set; }

    public decimal? NetDiscountPrice { get; set; }

    public decimal? NetDiscountPricePerUnit { get; set; }
}
