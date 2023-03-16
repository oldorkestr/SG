using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SGLNU.BLL.DTO
{
    public class UserDTO 
    {
        public UserDTO() { }
        public UserDTO(string id, string firstName, string lastName, int course, string faculty, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Course = course;
            Faculty = faculty;
            Email = email;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Course { get; set; }

        public string Faculty { get; set; }
        public string Email { get; set; }

    }
}
