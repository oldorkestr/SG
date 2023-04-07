using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SGLNU.BLL.DTO;

namespace SGLNU.Web.ViewModels
{
    public class VotingViewModel
    {
        public VotingViewModel()
        {
            
        }
        public VotingViewModel(int? id, string title, DateTime startDate, DateTime endDate, string facultyName, int? facultyId, bool isActive,
            ICollection<CandidateViewModel> candidates, ICollection<VoteViewModel> votes, string message)
        {
            Id = id;
            Title = title;
            StartDate = startDate;
            EndDate = endDate;
            FacultyName = facultyName;
            FacultyId = facultyId;
            IsActive = IsActive;
            Candidates = candidates;
            Votes = votes;
            Message = message;
            isActive = isActive;
        }

        public string Message { get; set; }

        public int? Id { get; set; }

        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Дата старту")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата завершення")]
        public DateTime EndDate { get; set; }
        
        public string FacultyName { get; set; }

        public int? FacultyId { get; set; }

        public bool IsActive { get; set; }

        public ICollection<CandidateViewModel> Candidates { get; set; }

        public ICollection<VoteViewModel> Votes { get; set; }
    }
}
