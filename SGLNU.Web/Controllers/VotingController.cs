using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGLNU.BLL.DTO;
using SGLNU.BLL.Interfaces;
using SGLNU.DAL.Entities;
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
        [AllowAnonymous]
        public IActionResult Votings()
        {
            var votings = User.Identity.IsAuthenticated ?
                votingService.GetAllVotings()
                : votingService.GetAllActiveVotings();

            return View(new VotingsViewModel() { Votings = votings });
        }

        [HttpGet]
        [Route("voting/votingEdit/{votingId}")]
        public IActionResult VotingEdit(int? votingId)
        {
            if (votingId.HasValue)
            {
                var voting = votingService.GetVoting(votingId.Value);
                return View(_mapper.Map<VotingDTO, VotingViewModel>(voting));
            }
            else
            {
                return View(new VotingViewModel());
            }
        }

        [HttpPost]
        [Route("voting/votingEdit/{votingId}")]
        public IActionResult VotingEdit(VotingViewModel voting)
        {
            if (voting.Id.HasValue)
            {
                votingService.UpdateVoting(_mapper.Map<VotingViewModel, VotingDTO>(voting));
            }
            else
            {
                votingService.CreateVoting(_mapper.Map<VotingViewModel, VotingDTO>(voting));
            }

            return RedirectToAction("Votings");
        }

        [HttpGet]
        [Route("voting/ActivateVoting/{votingId}")]
        public IActionResult ActivateVoting(int? votingId)
        {
            votingService.ActivateVoting(votingId.Value);

            return RedirectToAction("Votings");
        }

        [HttpGet]
        [Route("voting/DeActivateVoting/{votingId}")]
        public IActionResult DeActivateVoting(int? votingId)
        {
            votingService.DeActivateVoting(votingId.Value);

            return RedirectToAction("Votings");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("voting/voting/{votingId}")]
        public IActionResult Voting(int? votingId)
        {
            return View(_mapper.Map<VotingDTO, VotingViewModel>(votingService.GetVoting(votingId.Value)));
        }

        [HttpPost]
        public IActionResult AddCandidate(CandidateDTO candidate)
        {
            votingService.AddCandidate(candidate);

            return View(_mapper.Map<VotingDTO, VotingViewModel>(votingService.GetVoting(candidate.VotingId)));
        }

        [HttpPost]
        public IActionResult RemoveCandidate(CandidateDTO candidate)
        {
            votingService.RemoveCandidate(candidate);

            return View(_mapper.Map<VotingDTO, VotingViewModel>(votingService.GetVoting(candidate.VotingId)));
        }

        [HttpPost]
        public IActionResult AddVote(VotingDTO votingDto)
        {
            string message = "You are not eligible to vote here";
            if (User.HasClaim(c => c.Value == votingDto.Faculty.Name))
            {
                votingService.AddVote(votingDto, User.Identity.Name);
                message = "Vote accepted";
            }

            return View(_mapper.Map<VotingDTO, VotingViewModel>(votingService.GetVoting(votingDto.Id)));
        }

    }


}
