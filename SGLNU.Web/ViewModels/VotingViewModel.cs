using System;
using System.Collections.Generic;
using SGLNU.BLL.DTO;

namespace SGLNU.Web.ViewModels
{
    public class VotingViewModel
    {
        public VotingViewModel()
        {
            
        }
        public VotingViewModel(int? id, string title, DateTime startDate, DateTime endDate, FacultyDTO facultyDTO, bool isActive,
            ICollection<CandidateViewModel> candidates, ICollection<VoteViewModel> votes, string message)
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
            Message = message;
        }

        public string Message { get; set; }

        public int? Id { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public FacultyDTO Faculty { get; set; }

        public int? FacultyId { get; set; }

        public bool IsActive { get; set; }

        public ICollection<CandidateViewModel> Candidates { get; set; }

        public ICollection<VoteViewModel> Votes { get; set; }
    }
}
