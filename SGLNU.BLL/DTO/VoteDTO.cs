using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGLNU.BLL.DTO
{
    public class VoteDTO
    {
        public VoteDTO() { }
        public VoteDTO(string authorEmail, int candidateId)
        {
            AuthorEmail = authorEmail;
            CandidateId = candidateId;
        }
        public int Id { get; set; }

        public string AuthorEmail { get; set; }

        public int CandidateId { get; set; }
    }
}
