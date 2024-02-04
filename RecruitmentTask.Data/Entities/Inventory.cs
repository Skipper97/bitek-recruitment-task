using System;
using System.Collections.Generic;

namespace RecruitmentTask.Data.Entities;

public partial class Inventory
{
    public int Id { get; set; }

    public string Sku { get; set; } = null!;

    public string? Unit { get; set; }

    public decimal? Quantity { get; set; }

    public string? Manufacturer { get; set; }

    public string? ShippingTime { get; set; }

    public decimal? ShippingCost { get; set; }
}
