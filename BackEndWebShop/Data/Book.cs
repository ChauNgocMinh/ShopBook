using System;
using System.Collections.Generic;

namespace BackEndWebShop.Data;

public partial class Book
{
    public string Id { get; set; } = null!;

    public string? Namebook { get; set; }

    public string? Category { get; set; }

    public int Price { get; set; }

    public int? Status { get; set; }
}
