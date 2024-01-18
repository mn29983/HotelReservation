using System;
using Microsoft.AspNetCore.Identity;

namespace HotelReservation.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; }
    }
}


