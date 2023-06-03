using Microsoft.AspNetCore.Identity;

namespace BackEndWebShop.Data;

    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool Activate { get; set; }
    }

