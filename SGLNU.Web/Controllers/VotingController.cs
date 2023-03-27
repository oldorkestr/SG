using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SGLNU.BLL.Interfaces;
using SGLNU.DAL.Interfaces;
using SGLNU.Web.ViewModels;

namespace SGLNU.Web.Controllers
{
    public class VotingController : Controller
    {
        private readonly IVotingService votingService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VotingController(IVotingService votingService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.votingService = votingService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Votings()
        {
            var votings = votingService.GetAllVotings();

            return View(new VotingViewModel(){Votings = votings });
        }
    }


}
