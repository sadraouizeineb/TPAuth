using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;


namespace tpAuth.Models
{

    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }  
    }


}
