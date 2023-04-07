using System.Collections.Generic;


namespace SGLNU.Web.ViewModels
{
    public class CandidateViewModel
    {
        public CandidateViewModel()
        {
        }

        public CandidateViewModel(int id, string email, string programShort, string programExtended, int votingId, IEnumerable<VoteViewModel> votes)
        {
            Id = id;
            Email = email;
            ProgramShort = programShort;
            ProgramExtended = programExtended;
            VotingId = votingId;
            Votes = votes;
        }

        public int? Id { get; set; }

        public string Email { get; set; }

        public string ProgramShort { get; set; }

        public string ProgramExtended { get; set; }

        public int VotingId { get; set; }

        public IEnumerable<VoteViewModel> Votes { get; set; }
    }
}
