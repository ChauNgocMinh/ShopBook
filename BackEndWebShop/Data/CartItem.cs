using System;
using System.Collections.Generic;

namespace BackEndWebShop.Data;

public partial class CartItem
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? IdBook { get; set; } = null!;

    public double? TotalItem { get; set; } = null!;

    public int? Number { get; set; } = null!;

    public bool? Status { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual ICollection<Bill> Bills { get; } = new List<Bill>();

    public virtual Book? IdBookNavigation { get; set; }
}
