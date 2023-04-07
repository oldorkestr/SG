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
    public class VotingService: IVotingService
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
            _unitOfWork.Votings.Update(votingEntity);
            _unitOfWork.Save();
        }

        public void ActivateVoting(int votingId)
        {
            var votingEntity = _unitOfWork.Votings.Get(votingId);
            votingEntity.IsActive = true;
            _unitOfWork.Votings.Update(votingEntity);
            _unitOfWork.Save();
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
            Candidate candidateEntity = null;
            if (votingCandidateDTO.Id != null)
            {
                candidateEntity = _unitOfWork.Candidates.Get(votingCandidateDTO.Id.Value);
            }
            candidateEntity ??= _unitOfWork.Candidates
                .Create(new Candidate()
                {
                    Email = votingCandidateDTO.Email,
                    ProgramShort = votingCandidateDTO.ProgramShort,
                    ProgramExtended = votingCandidateDTO.ProgramExtended,
                    VotingId = votingCandidateDTO.VotingId
                });
            if (!votingEntity.Candidates.Contains(candidateEntity))
            {
                votingEntity.Candidates.Add(candidateEntity);
                candidateEntity.VotingId = votingEntity.Id;
                _unitOfWork.Candidates.Update(candidateEntity);
                _unitOfWork.Votings.Update(votingEntity);
                _unitOfWork.Save();
            }

            return _mapper.Map<Voting, VotingDTO>(votingEntity);
        }

        public VotingDTO RemoveCandidate(CandidateDTO votingCandidateDTO)
        {
            var votingEntity = _unitOfWork.Votings.Get(votingCandidateDTO.VotingId);
            Candidate candidateEntity = null;
            if (votingCandidateDTO.Id != null)
            {
                candidateEntity = _unitOfWork.Candidates.Get(votingCandidateDTO.Id.Value);
            }
            if (candidateEntity != null && votingEntity.Candidates.Contains(candidateEntity))
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

        public VotingDTO AddVote(VotingDTO VotingDTO, string userEmail)
        {
            var votingEntity = _unitOfWork.Votings.Get(VotingDTO.Id);
            //if (!votingEntity.Candidates.Select(c => c.Email).Contains(userEmail))
            //{
            //    Vote newVoteEntity = _unitOfWork.Votes
            //        .Create(new Vote()
            //        {
            //            VotingId = votingEntity.Id,
            //            AuthorEmail = userEmail
            //        });
            //    votingEntity.Votes.Add(newVoteEntity);
            //    _unitOfWork.Votings.Update(votingEntity);
            //    _unitOfWork.Save();
            //}

            return _mapper.Map<Voting, VotingDTO>(votingEntity);
        }
    }
}
