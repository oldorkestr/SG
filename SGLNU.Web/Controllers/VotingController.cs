using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SGLNU.BLL.DTO;
using SGLNU.BLL.Interfaces;
using SGLNU.BLL.Services;
using SGLNU.DAL.Entities;
using SGLNU.DAL.Interfaces;
using SGLNU.Web.ViewModels;

namespace SGLNU.Web.Controllers
{
    public class VotingController : Controller
    {
        private readonly IVotingService votingService;
        private readonly ICandidateService candidateService;
        private readonly IFacultyService facultyService;
        private readonly IMapper _mapper;

        public VotingController(IVotingService votingService, IMapper mapper, ICandidateService candidateService, IFacultyService facultyService)
        {
            this.votingService = votingService;
            this._mapper = mapper;
            this.candidateService = candidateService;
            this.facultyService = facultyService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("votings/votings")]
        public IActionResult Votings()
        {
            var votings = User.Identity.IsAuthenticated ?
                votingService.GetAllVotings()
                : votingService.GetAllActiveVotings();

            return View(new VotingsViewModel() { Votings = votings });
        }

        [HttpGet]
        [Route("voting/Edit/{id}")]
        public IActionResult VotingEdit(int id)
        {
            ViewBag.Faculties = _mapper.Map<IEnumerable<FacultyDTO>, IEnumerable<FacultyViewModel>>(facultyService.GetAllFaculties());
            if (id > 0)
            {
                var voting = votingService.GetVoting(id);
                return View(_mapper.Map<VotingDTO, VotingViewModel>(voting));
            }
            else
            {
                var voting = new VotingViewModel()
                {
                    Candidates = new List<CandidateViewModel>()
                };
                return View(voting);
            }
        }

        [HttpPost]
        [Route("voting/Edit/{id}")]
        public IActionResult VotingEdit(VotingViewModel voting)
        {
            if (voting.Id != 0)
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
        [Route("voting/Delete/{votingId}")]
        public IActionResult DeleteVoting(int? votingId)
        {
            votingService.DeleteVoting(votingId.Value);

            return RedirectToAction("Votings");
        }

        [HttpGet]
        [Route("voting/Activate/{votingId}")]
        public IActionResult ActivateVoting(int? votingId)
        {
            votingService.ActivateVoting(votingId.Value);

            return RedirectToAction("Votings");
        }

        [HttpGet]
        [Route("voting/DeActivate/{votingId}")]
        public IActionResult DeActivateVoting(int? votingId)
        {
            votingService.DeActivateVoting(votingId.Value);

            return RedirectToAction("Votings");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("voting/View/{id}")]
        public IActionResult Voting(int? id)
        {
            var voting = _mapper.Map<VotingDTO, VotingViewModel>(votingService.GetVoting(id.Value));
            if (!votingService.VotingAvailable(id.Value, User.Identity.Name))
            {
                voting.Message = "Forbidden";
            }
            return View(voting);
        }

        [HttpGet]
        [Route("voting/EditCandidate/{votingId}")]
        public IActionResult EditCandidate(int votingId, int? candidateId)
        {
            if (candidateId != null)
            {
                return View(_mapper.Map<CandidateDTO, CandidateViewModel>(candidateService.GetCandidate(candidateId.Value)));
            }
            else
            {
                return View(new CandidateViewModel() { VotingId = votingId });
            }
        }

        [HttpPost]
        [Route("voting/EditCandidate/{votingId}")]
        public IActionResult EditCandidate(CandidateViewModel candidate, int votingId)
        {
            var candidateDto = _mapper.Map<CandidateViewModel, CandidateDTO>(candidate);
            var file = Request.Form.Files["Photo"];
            if (file != null && file.Length < 1000000 && IsImage(file))
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    candidateDto.Photo = ms.ToArray();
                }
            }

            if (candidate.Id.HasValue)
            {
                candidateService.UpdateCandidate(candidateDto);
            }
            else
            {
                votingService.AddCandidate(candidateDto);
            }   

            return RedirectToAction("VotingEdit", new { id = votingId });
        }

        [HttpGet]
        [Route("voting/RemoveCandidate")]
        public IActionResult RemoveCandidate(int candidateId)
        {
            var voting = votingService.RemoveCandidate(candidateId);
            return RedirectToAction("VotingEdit", new { id = voting.Id });
        }

        [HttpGet]
        [Route("voting/vote/{votingId}")]
        public IActionResult AddVote(int votingId, int candidateId)
        {
            string message = "You are not eligible to vote here";
            //if (User.HasClaim(c => c.Value == votingDto.Faculty.Name))
            {
                votingService.AddVote(votingId, candidateId, User.Identity.Name);
                message = "Vote accepted";
            }

            return RedirectToAction("View", new { id = votingId });
        }


        private bool IsImage(IFormFile file)
        {
            return ((file != null) &&
                    Regex.IsMatch(file.ContentType, "image/\\S+") &&
                    (file.Length > 0));
        }
    }


}
