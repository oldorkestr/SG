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
        public int? Id { get; set; }

        public string Email { get; set; }

        public string ProgramShort { get; set; }

        public string ProgramExtended { get; set; }

        public int VotingId { get; set; }

        public IEnumerable<Vote> Votes { get; set; }
    }
}
