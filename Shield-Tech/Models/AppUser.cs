using Microsoft.AspNetCore.Identity;

namespace Shield_Tech.Models
{
    public class AppUser: IdentityUser
    {

     
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
