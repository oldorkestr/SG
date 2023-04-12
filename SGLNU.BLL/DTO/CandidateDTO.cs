using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGLNU.DAL.Entities;

namespace SGLNU.BLL.DTO
{
    public class CandidateDTO
    {
        public CandidateDTO() { }
        public CandidateDTO(string email, string firstName, string lastName,
            byte[] photo, string programShort, string programExtended,
            int votingId, IEnumerable<VoteDTO> votes)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Votes = votes;
            Photo = photo;
            ProgramShort = programShort;
            ProgramExtended = programExtended;
            VotingId = votingId;
        }
        public int? Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] Photo { get; set; }

        public string ProgramShort { get; set; }

        public string ProgramExtended { get; set; }

        public int VotingId { get; set; }

        public IEnumerable<VoteDTO> Votes { get; set; }
    }
}
