using Microsoft.AspNetCore.Identity;

namespace FoodApp.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
