using System;
using System.Collections.Generic;
using System.Text;

namespace SGLNU.BLL.DTO
{
    public class FacultyDTO
    {
        public FacultyDTO() { }

        public FacultyDTO(int id, string name, string email, string description, string logoFilePath, int universityId)
        {
            Id = id;
            Name = name;
            Email = email;
            Description = description;
            LogoFilePath = logoFilePath;
            UniversityId = universityId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string LogoFilePath { get; set; }
        public int UniversityId { get; set; }
    }
}

