using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGLNU.BLL.DTO;

namespace SGLNU.BLL.Interfaces
{
    public interface ICandidateService
    {
        IEnumerable<CandidateDTO> GetAllCandidates();

        IEnumerable<CandidateDTO> GetCandidates(int votingId);

        public CandidateDTO GetCandidate(int candidateId);

        public void UpdateCandidate(CandidateDTO candidateDTO);

        public void DeleteCandidate(int candidateId);
    }
}
