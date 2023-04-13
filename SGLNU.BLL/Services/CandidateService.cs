using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.BLL.Interfaces;
using SGLNU.DAL.Entities;
using SGLNU.DAL.Interfaces;

namespace SGLNU.BLL.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CandidateService() { }
        public CandidateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<CandidateDTO> GetAllCandidates()
        {
            return _mapper.Map<IEnumerable<Candidate>, IEnumerable<CandidateDTO>>(
                _unitOfWork.Candidates.GetAll());
        }

        public IEnumerable<CandidateDTO> GetCandidates(int votingId)
        {
            if (_unitOfWork.Votings.Get(votingId) != null)
            {
                return _mapper.Map<IEnumerable<Candidate>, IEnumerable<CandidateDTO>>(
                _unitOfWork.Candidates.GetAll().Where(c => c.VotingId == votingId));
            }
            else
            {
                throw new ArgumentException(nameof(votingId));
            }
        }

        public CandidateDTO GetCandidate(int candidateId)
        {
            var candidate = _unitOfWork.Candidates.Get(candidateId);
            if (candidate != null)
            {
                return _mapper.Map<Candidate, CandidateDTO>(candidate);
            }
            else
            {
                throw new ArgumentException(nameof(candidateId));
            }
        }

        public void UpdateCandidate(CandidateDTO candidateDTO)
        {
            var candidateEntity = _unitOfWork.Candidates.Get(candidateDTO.Id.Value);
            if (candidateEntity != null)
            {
                candidateEntity.Email = candidateDTO.Email;
                candidateEntity.FirstName = candidateDTO.FirstName;
                candidateEntity.LastName = candidateDTO.LastName;
                candidateEntity.ProgramShort = candidateDTO.ProgramShort;
                candidateEntity.ProgramExtended = candidateDTO.ProgramExtended;
                if (candidateDTO.Photo.Length > 0)
                {
                    candidateEntity.Photo = candidateDTO.Photo;
                }
                _unitOfWork.Candidates.Update(candidateEntity);
                _unitOfWork.Save();
            }
            else
            {
                throw new ArgumentException(nameof(candidateDTO));
            }
        }

        public void DeleteCandidate(int candidateId)
        {
            if (_unitOfWork.Candidates.Get(candidateId) != null)
            {
                var voteEntities = _unitOfWork.Votes.Find(v => v.CandidateId == candidateId);
                foreach (var voteEntity in voteEntities)
                {
                    _unitOfWork.Votes.Delete(voteEntity.Id);
                }
                _unitOfWork.Candidates.Delete(candidateId);
                _unitOfWork.Save();
            }
            else
            {
                throw new ArgumentException(nameof(candidateId));
            }

        }
    }
}
