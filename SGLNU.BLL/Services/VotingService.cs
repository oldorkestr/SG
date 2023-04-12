using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.BLL.Interfaces;
using SGLNU.DAL.Entities;
using SGLNU.DAL.Interfaces;

namespace SGLNU.BLL.Services
{
    public class VotingService : IVotingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VotingService() { }
        public VotingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<VotingDTO> GetAllVotings()
        {
            var votingEntities = _unitOfWork.Votings.GetAll().ToList();
            return _mapper.Map<IEnumerable<Voting>, IEnumerable<VotingDTO>>(votingEntities);
        }

        public IEnumerable<VotingDTO> GetAllActiveVotings()
        {
            return _mapper.Map<IEnumerable<Voting>, List<VotingDTO>>(
                _unitOfWork.Votings
                    .GetAll()
                    .Where(v => v.IsActive)
                );
        }

        public IEnumerable<VotingDTO> GetFutureVotings()
        {
            return _mapper.Map<IEnumerable<Voting>, List<VotingDTO>>(
                _unitOfWork.Votings
                    .GetAll()
                    .Where(v => v.StartDate.AddDays(1) >= DateTime.Now)
            );
        }

        public IEnumerable<VotingDTO> GetPastVotings()
        {
            return _mapper.Map<IEnumerable<Voting>, List<VotingDTO>>(
                _unitOfWork.Votings
                    .GetAll()
                    .Where(v => v.EndDate.AddDays(-1) <= DateTime.Now)
            );
        }

        public VotingDTO CreateVoting(VotingDTO votingDTO)
        {
            if (votingDTO != null)
            {
                Voting voting = _mapper.Map<Voting>(votingDTO);
                var newVotingEntity = _unitOfWork.Votings.Create(voting);
                _unitOfWork.Save();
                return _mapper.Map<Voting, VotingDTO>(newVotingEntity);
            }
            else
            {
                //_logger.LogWarning("Could not create new question.");
                throw new ArgumentNullException(nameof(votingDTO));
            }
        }

        public VotingDTO GetVoting(int id)
        {
            var votingEntity = _unitOfWork.Votings.Get(id);
            return _mapper.Map<Voting, VotingDTO>(votingEntity);
        }

        public void UpdateVoting(VotingDTO votingDTO)
        {
            var votingEntity = _unitOfWork.Votings.Get(votingDTO.Id);
            votingEntity.StartDate = votingDTO.StartDate;
            votingEntity.EndDate = votingDTO.EndDate;
            votingEntity.Title = votingDTO.Title;
            votingEntity.FacultyId = votingDTO.FacultyId;
            _unitOfWork.Votings.Update(votingEntity);
            _unitOfWork.Save();
        }

        public void ActivateVoting(int votingId)
        {
            var votingEntity = _unitOfWork.Votings.Get(votingId);
            if (votingEntity.Candidates.Any())
            {
                votingEntity.IsActive = true;
                _unitOfWork.Votings.Update(votingEntity);
                _unitOfWork.Save();
            }
        }

        public void DeActivateVoting(int votingId)
        {
            var votingEntity = _unitOfWork.Votings.Get(votingId);
            votingEntity.IsActive = false;
            _unitOfWork.Votings.Update(votingEntity);
            _unitOfWork.Save();
        }

        public VotingDTO AddCandidate(CandidateDTO votingCandidateDTO)
        {
            var votingEntity = _unitOfWork.Votings.Get(votingCandidateDTO.VotingId);
            if (!votingEntity.Candidates.Select(c => c.Email.ToLower()).Contains(votingCandidateDTO.Email.ToLower()))
            {
                Candidate candidateEntity = _unitOfWork.Candidates
                                    .Create(_mapper.Map<CandidateDTO, Candidate>(votingCandidateDTO));

                votingEntity.Candidates.Add(candidateEntity);
                _unitOfWork.Votings.Update(votingEntity);
                _unitOfWork.Save();
            }

            return _mapper.Map<Voting, VotingDTO>(votingEntity);
        }

        public VotingDTO RemoveCandidate(int candidateId)
        {
            Candidate candidateEntity = _unitOfWork.Candidates.Get(candidateId);
            var votingEntity = _unitOfWork.Votings.Get(candidateEntity.VotingId);
            if (!votingEntity.IsActive)
            {
                foreach (var vote in candidateEntity.Votes)
                {
                    _unitOfWork.Votes.Delete(vote.Id);
                }
                candidateEntity.Votes.Clear();
                votingEntity.Candidates.Remove(candidateEntity);
                _unitOfWork.Candidates.Delete(candidateEntity.Id);
                _unitOfWork.Votings.Update(votingEntity);
                _unitOfWork.Save();
            }
            return _mapper.Map<Voting, VotingDTO>(votingEntity);
        }

        public VotingDTO AddVote(int votingId, int candidateId, string userEmail)
        {
            var votingEntity = _unitOfWork.Votings.Get(votingId);
            Candidate candidateEntity = _unitOfWork.Candidates.Get(candidateId);

            if (VotingAvailable(votingEntity, userEmail))
            {
                Vote newVoteEntity = _unitOfWork.Votes
                    .Create(new Vote()
                    {
                        CandidateId = candidateId,
                        AuthorEmail = userEmail
                    });
                candidateEntity.Votes.Add(newVoteEntity);
                _unitOfWork.Candidates.Update(candidateEntity);
                _unitOfWork.Save();
            }

            return _mapper.Map<Voting, VotingDTO>(votingEntity);
        }

        public bool VotingAvailable(int votingId, string userEmail)
        {
            return VotingAvailable(_unitOfWork.Votings.Get(votingId), userEmail);
        }

        public void DeleteVoting(int votingId)
        {
            if (_unitOfWork.Votings.Get(votingId) != null)
            {
                _unitOfWork.Votings.Delete(votingId);
                _unitOfWork.Save();
            }
            else
            {
                //_logger.LogWarning("Could not create new question.");
                throw new ArgumentException(nameof(votingId));
            }
        }

        private bool VotingAvailable(Voting voting, string userEmail)
        {
            var votes = new List<Vote>();
            foreach (var candidate in voting.Candidates)
            {
                if (candidate.Votes != null)
                {
                    votes.AddRange(candidate.Votes);
                }
            }

            var hadCandidates = voting.Candidates.Any();
            var alreadyVoted = votes.Select(v => v.AuthorEmail).Contains(userEmail);
            return !alreadyVoted
                || !hadCandidates;
        }
    }
}
