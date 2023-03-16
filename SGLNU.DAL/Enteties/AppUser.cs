using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGLNU.DAL.Enteties
{
    public class AppUser : IdentityUser
    {
        public AppUser() { }
        public AppUser(string firstName, string lastName, int course)
        {
            FirstName = firstName;
            LastName = lastName;
            Course = course;
        }

        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }

        [PersonalData]
        public int Course { get; set; }
    }
}
