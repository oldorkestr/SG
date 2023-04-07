using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Entities;

namespace SGLNU.BLL.Interfaces
{
    public interface IVotingService
    {
        public IEnumerable<VotingDTO> GetAllVotings();

        public IEnumerable<VotingDTO> GetAllActiveVotings();

        public IEnumerable<VotingDTO> GetFutureVotings();

        public IEnumerable<VotingDTO> GetPastVotings();

        public VotingDTO GetVoting(int id);

        public VotingDTO CreateVoting(VotingDTO VotingDTO);

        public void UpdateVoting(VotingDTO VotingDTO);

        public void ActivateVoting(int votingId);

        public void DeActivateVoting(int votingId);

        public VotingDTO AddCandidate(CandidateDTO votingCandidateDTO);

        public VotingDTO RemoveCandidate(int candidateId);

        public VotingDTO AddVote(int votingId, int candidateId, string userEmail);

        public bool VotingAvailable(int votingId, string userEmail);
        
        public void DeleteVoting(int votingId);
    }
}
