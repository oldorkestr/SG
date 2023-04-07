using System;
using System.Collections.Generic;

namespace SGLNU.BLL.DTO
{
    public class VotingDTO
    {
        public VotingDTO() { }
        public VotingDTO(int id, string title, DateTime startDate, DateTime endDate, FacultyDTO facultyDTO, bool isActive,
            ICollection<CandidateDTO> candidates, ICollection<VoteDTO> votes)
        {
            Id = id;
            Title = title;
            StartDate = startDate;
            EndDate = endDate;
            Faculty = facultyDTO;
            FacultyId = facultyDTO?.Id;
            IsActive = IsActive;
            Candidates = candidates;
            Votes = votes;
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public FacultyDTO Faculty { get; set; }

        public int? FacultyId { get; set; }

        public bool IsActive { get; set; }

        public ICollection<CandidateDTO> Candidates { get; set; }

        public ICollection<VoteDTO> Votes { get; set; }
    }
}
