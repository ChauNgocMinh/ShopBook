using System;
using System.Collections.Generic;

namespace BackEndWebShop.Data;

public partial class AspNetUserRole
{
    public string UserId { get; set; } = null!;

    public string RoleId { get; set; } = null!;

    public virtual ICollection<AspNetRole> Roles { get; } = new List<AspNetRole>();

    public virtual AspNetUser User { get; set; } = null!;
}
