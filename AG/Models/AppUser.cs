using Microsoft.AspNetCore.Identity;

namespace AG.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser() :base()
        {
            
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
