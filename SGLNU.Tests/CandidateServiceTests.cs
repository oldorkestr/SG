using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework.Constraints;
using SGLNU.BLL.DTO;
using SGLNU.BLL.Services;
using SGLNU.DAL.Entities;
using SGLNU.DAL.Interfaces;

namespace SGLNU.Tests
{
    public class CandidateServiceTests
    {
        private CandidateService _candidateService;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IMapper> _mockMapper;
        private IMapper _mapper;

        private List<Candidate> _candidateEntities;
        private List<CandidateDTO> _expectedandidateDTOs;
        private List<Candidate> _votingCandidateEntities;
        private List<CandidateDTO> _expectedVotingCandidatesDTOs;
        [SetUp]
        public void SetUp()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _candidateService = new CandidateService(_mockUnitOfWork.Object, _mockMapper.Object);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Candidate, CandidateDTO>();
                cfg.CreateMap<CandidateDTO, Candidate>();
                cfg.CreateMap<VoteDTO, Vote>();
                cfg.CreateMap<Vote, VoteDTO>();
            });
            _mapper = config.CreateMapper();

            _candidateEntities = new List<Candidate>
            {
                new Candidate {
                    Id = 1,
                    FirstName = "Орест",
                    LastName = "Онищенко",
                    Email = "orest.onyshchenko@lnu.edu.ua",
                    ProgramShort = "short",
                    ProgramExtended = "extended",
                    Photo= Array.Empty<byte>(),
                    VotingId = 1,
                    Votes = new List<Vote>()
                },
                new Candidate {
                    Id = 2,
                    FirstName = "Остап",
                    LastName = "Левицький",
                    Email = "ostap.levytskyi@lnu.edu.ua",
                    ProgramShort = "short",
                    ProgramExtended = "extended",
                    Photo= Array.Empty<byte>(),
                    VotingId = 1,
                    Votes = new List<Vote>()
                },
                new Candidate {
                    Id = 3,
                    FirstName = "Остап",
                    LastName = "Левицький",
                    Email = "ostap.levytskyi@lnu.edu.ua",
                    ProgramShort = "short",
                    ProgramExtended = "extended",
                    Photo= Array.Empty<byte>(),
                    VotingId = 2,
                    Votes = new List<Vote>()
                }
            };
            _expectedandidateDTOs = new List<CandidateDTO>
            {
                new CandidateDTO {
                    Id = 1,
                    FirstName = "Орест",
                    LastName = "Онищенко",
                    Email = "orest.onyshchenko@lnu.edu.ua",
                    ProgramShort = "short",
                    ProgramExtended = "extended",
                    Photo= Array.Empty<byte>(),
                    VotingId = 1,
                    Votes = new List<VoteDTO>()
                },
                new CandidateDTO {
                    Id = 2,
                    FirstName = "Остап",
                    LastName = "Левицький",
                    Email = "ostap.levytskyi@lnu.edu.ua",
                    ProgramShort = "short",
                    ProgramExtended = "extended",
                    Photo= Array.Empty<byte>(),
                    VotingId = 1,
                    Votes = new List<VoteDTO>()
                },
                new CandidateDTO {
                    Id = 3,
                    FirstName = "Остап",
                    LastName = "Левицький",
                    Email = "ostap.levytskyi@lnu.edu.ua",
                    ProgramShort = "short",
                    ProgramExtended = "extended",
                    Photo= Array.Empty<byte>(),
                    VotingId = 2,
                    Votes = new List<VoteDTO>()
                }
            };
            _votingCandidateEntities = _candidateEntities.Where(c => c.VotingId == 1).ToList();
            _expectedVotingCandidatesDTOs = _expectedandidateDTOs.Where(c => c.VotingId == 1).ToList();
        }

        [Test]
        public void GetAllCandidates_ShouldReturnCorrectCandidates()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.Candidates.GetAll()).Returns(_candidateEntities);
            _mockMapper.Setup(m => m.Map<IEnumerable<Candidate>, IEnumerable<CandidateDTO>>(_candidateEntities))
                .Returns(_expectedandidateDTOs);

            // Act
            var result = _candidateService.GetAllCandidates();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_expectedandidateDTOs.Count, result.Count());
            foreach (var candidateEntity in _candidateEntities)
            {
                var candidateDTO = _expectedandidateDTOs.FirstOrDefault(c => c.Id == candidateEntity.Id);
                Assert.NotNull(candidateDTO);
                Assert.AreEqual(candidateDTO?.FirstName, candidateEntity.FirstName);
                Assert.AreEqual(candidateDTO?.LastName, candidateEntity.LastName);
                Assert.AreEqual(candidateDTO?.Email, candidateEntity.Email);
                Assert.AreEqual(candidateDTO?.ProgramShort, candidateEntity.ProgramShort);
                Assert.AreEqual(candidateDTO?.ProgramExtended, candidateEntity.ProgramExtended);
                Assert.AreEqual(candidateDTO?.VotingId, candidateEntity.VotingId);
                Assert.AreEqual(candidateDTO?.Votes.Count(), candidateEntity.Votes.Count);
            }
            Assert.IsTrue(_expectedandidateDTOs.SequenceEqual(result));
        }

        [Test]
        public void GetVotingCandidates_ShouldReturnCorrectCandidates()
        {
            var votingId = 1;
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.Candidates.GetAll()).Returns(_votingCandidateEntities);
            _mockMapper.Setup(m => m.Map<IEnumerable<Candidate>, IEnumerable<CandidateDTO>>(_votingCandidateEntities))
                .Returns(_expectedVotingCandidatesDTOs);

            // Act
            var result = _candidateService.GetCandidates(votingId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_expectedVotingCandidatesDTOs.Count, result.Count());
            foreach (var candidateEntity in _candidateEntities)
            {
                var candidateDTO = _expectedandidateDTOs.FirstOrDefault(c => c.Id == candidateEntity.Id);
                Assert.NotNull(candidateDTO);
                Assert.AreEqual(candidateDTO?.FirstName, candidateEntity.FirstName);
                Assert.AreEqual(candidateDTO?.LastName, candidateEntity.LastName);
                Assert.AreEqual(candidateDTO?.Email, candidateEntity.Email);
                Assert.AreEqual(candidateDTO?.ProgramShort, candidateEntity.ProgramShort);
                Assert.AreEqual(candidateDTO?.ProgramExtended, candidateEntity.ProgramExtended);
                Assert.AreEqual(candidateDTO?.VotingId, candidateEntity.VotingId);
                Assert.AreEqual(candidateDTO?.Votes.Count(), candidateEntity.Votes.Count);
            }
            Assert.IsTrue(_expectedVotingCandidatesDTOs.SequenceEqual(result));
        }
    }
}
