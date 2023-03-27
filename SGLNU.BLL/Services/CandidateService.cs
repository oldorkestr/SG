using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.BLL.Interfaces;
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
            throw new NotImplementedException();
        }
    }
}
