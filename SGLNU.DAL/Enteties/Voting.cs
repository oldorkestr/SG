using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SGLNU.DAL.Entities;

namespace SGLNU.DAL.Entities
{
    public class Voting
    {
        [Key]
        public int Id { get; set; }
        
        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        
        public Faculty Faculty { get; set; }

        public int? FacultyId { get; set; }

        public bool IsActive{ get; set; }

        public ICollection<Candidate> Candidates { get; set; }

    }
}
