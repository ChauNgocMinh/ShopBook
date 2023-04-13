using System;
using System.Collections.Generic;

namespace BackEndWebShop.Data;

public partial class CartItem
{
    public string IdCartItem { get; set; } = null!;

    public string? IdBook { get; set; }

    public string? IdUser { get; set; }

    public int? Number { get; set; }

    public double? TotalItem { get; set; }

    public virtual Book? IdBookNavigation { get; set; }

    public virtual AspNetUser? IdUserNavigation { get; set; }
}
