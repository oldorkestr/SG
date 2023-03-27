using Microsoft.AspNetCore.Identity;

namespace SGLNU.DAL.Entities
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
