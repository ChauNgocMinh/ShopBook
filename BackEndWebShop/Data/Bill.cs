using System;
using System.Collections.Generic;

namespace BackEndWebShop.Data;

public partial class Bill
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string IdCartItem { get; set; } = null!;

    public DateTime BuyingDate { get; set; }

    public virtual CartItem IdNavigation { get; set; } = null!;
}
