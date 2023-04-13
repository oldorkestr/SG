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
        private Candidate _candidateEntity;
        private CandidateDTO _expectedCandidateDTO;
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
                    Votes = new List<Vote>
                    {
                        new Vote(){Id = 1, AuthorEmail = "orest.onyshchenko@lnu.edu.ua", CandidateId = 1},
                        new Vote(){Id = 2, AuthorEmail = "ostap.levytskyi@lnu.edu.ua", CandidateId = 1}
                    }
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
                    Votes = new List<VoteDTO>(){
                        new VoteDTO {Id = 1, AuthorEmail = "orest.onyshchenko@lnu.edu.ua", CandidateId = 1},
                        new VoteDTO {Id = 2, AuthorEmail = "ostap.levytskyi@lnu.edu.ua", CandidateId = 1}
                    }
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
            _candidateEntity = _candidateEntities.FirstOrDefault();
            _expectedCandidateDTO = _expectedandidateDTOs.FirstOrDefault();
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
            _mockUnitOfWork.Setup(uow => uow.Votings.Get(votingId)).Returns(new Voting() { Id = votingId });
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

        [Test]
        public void GetVotingCandidates_WithVotingNotFound_ShouldThrowArgumentException()
        {
            var votingId = 1;
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.Votings.Get(votingId)).Returns((Voting)null);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _candidateService.GetCandidates(votingId));
        }

        [Test]
        public void GetCandidates_ShouldReturnCorrectCandidateData()
        {
            var candidateId = 1;
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.Candidates.Get(candidateId)).Returns(_candidateEntity);
            _mockMapper.Setup(m => m.Map<Candidate, CandidateDTO>(_candidateEntity))
                .Returns(_expectedCandidateDTO);

            // Act
            var result = _candidateService.GetCandidate(candidateId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_expectedCandidateDTO.Votes.Count(), result.Votes.Count());
            foreach (var voteEntity in _candidateEntity.Votes)
            {
                var voteDTO = _expectedCandidateDTO.Votes.FirstOrDefault(c => c.Id == voteEntity.Id);
                Assert.NotNull(voteDTO);
                Assert.AreEqual(voteDTO.CandidateId, voteEntity.CandidateId);
                Assert.AreEqual(voteDTO.AuthorEmail, voteEntity.AuthorEmail);
            }
            Assert.IsTrue(_expectedCandidateDTO.Votes.SequenceEqual(result.Votes));
        }

        [Test]
        public void GetCandidate_WithCandidateNotFound_ShouldThrowArgumentException()
        {
            var candidateId = 1;
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.Candidates.Get(candidateId)).Returns((Candidate)null);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _candidateService.GetCandidate(candidateId));
        }

        [Test]
        public void UpdateCandidates_ShouldUpdateCandidateData()
        {
            var candidateDTO = new CandidateDTO
            {
                Id = 1,
                FirstName = "Орест",
                LastName = "Онищенко",
                Email = "orest.onyshchenko@lnu.edu.ua",
                ProgramShort = "short",
                ProgramExtended = "extended",
                Photo = Array.Empty<byte>(),
                VotingId = 1,
                Votes = new List<VoteDTO>(){
                    new VoteDTO {Id = 1, AuthorEmail = "orest.onyshchenko@lnu.edu.ua", CandidateId = 1},
                    new VoteDTO {Id = 2, AuthorEmail = "ostap.levytskyi@lnu.edu.ua", CandidateId = 1}
                }
            };

            var candidateEntity = new Candidate
            {
                Id = 1,
                FirstName = "Орест1",
                LastName = "Онищенко1",
                Email = "orest.onyshchenko@lnu.edu.com ",
                ProgramShort = "short program",
                ProgramExtended = "extended program",
                Photo = Array.Empty<byte>(),
                VotingId = 1,
                Votes = new List<Vote>
                {
                    new Vote(){Id = 1, AuthorEmail = "orest.onyshchenko@lnu.edu.ua", CandidateId = 1},
                    new Vote(){Id = 2, AuthorEmail = "ostap.levytskyi@lnu.edu.ua", CandidateId = 1}
                }
            };

            _mockUnitOfWork.Setup(u => u.Candidates.Get(candidateDTO.Id.Value)).Returns(candidateEntity);

            // Act
            _candidateService.UpdateCandidate(candidateDTO);

            // Assert
            _mockUnitOfWork.Verify(u => u.Candidates.Update(It.IsAny<Candidate>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.Save(), Times.Once);

            Assert.AreEqual(candidateDTO.FirstName, candidateEntity.FirstName);
            Assert.AreEqual(candidateDTO.LastName, candidateEntity.LastName);
            Assert.AreEqual(candidateDTO.Email, candidateEntity.Email);
            Assert.AreEqual(candidateDTO.ProgramShort, candidateEntity.ProgramShort);
            Assert.AreEqual(candidateDTO.ProgramExtended, candidateEntity.ProgramExtended);
            Assert.AreEqual(candidateDTO.Photo, candidateEntity.Photo);
        }

        [Test]
        public void UpdateCandidates_WithNullCandidateDTO_ShouldThrowNullReferencException()
        {
            // Arrange
            CandidateDTO candidateDTO = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => _candidateService.UpdateCandidate(candidateDTO));
        }

        [Test]
        public void UpdateCandidates_WithNullCandidateId_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var candidateDTO = new CandidateDTO
            {
                Id = null,
                FirstName = "Орест",
                LastName = "Онищенко",
                Email = "orest.onyshchenko@lnu.edu.ua",
                ProgramShort = "short",
                ProgramExtended = "extended",
                Photo = Array.Empty<byte>(),
                VotingId = 1,
                Votes = new List<VoteDTO>(){
                    new VoteDTO {Id = 1, AuthorEmail = "orest.onyshchenko@lnu.edu.ua", CandidateId = 1},
                    new VoteDTO {Id = 2, AuthorEmail = "ostap.levytskyi@lnu.edu.ua", CandidateId = 1}
                }
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _candidateService.UpdateCandidate(candidateDTO));
        }

        [Test]
        public void UpdateCandidates_WithNotFoundCandidate_ShouldThrowArgumentException()
        {
            // Arrange
            var candidateDTO = new CandidateDTO
            {
                Id = 1,
                FirstName = "Орест",
                LastName = "Онищенко",
                Email = "orest.onyshchenko@lnu.edu.ua",
                ProgramShort = "short",
                ProgramExtended = "extended",
                Photo = Array.Empty<byte>(),
                VotingId = 1,
                Votes = new List<VoteDTO>(){
                    new VoteDTO {Id = 1, AuthorEmail = "orest.onyshchenko@lnu.edu.ua", CandidateId = 1},
                    new VoteDTO {Id = 2, AuthorEmail = "ostap.levytskyi@lnu.edu.ua", CandidateId = 1}
                }
            };

            _mockUnitOfWork.Setup(u => u.Candidates.Get(candidateDTO.Id.Value)).Returns((Candidate)null);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _candidateService.UpdateCandidate(candidateDTO));
        }

        [Test]
        public void DeleteCandidate_ValidCandidateId_CandidateDeleted()
        {
            // Arrange
            int candidateId = 1;
            _mockUnitOfWork.Setup(u => u.Votes.GetAll()).Returns(_candidateEntity.Votes);
            _mockUnitOfWork.Setup(u => u.Candidates.Get(candidateId)).Returns(_candidateEntity);

            // Act
            _candidateService.DeleteCandidate(candidateId);

            // Assert
            _mockUnitOfWork.Verify(u => u.Candidates.Delete(candidateId), Times.Once);
            _mockUnitOfWork.Verify(u => u.Save(), Times.Once);
        }

        [Test]
        public void DeleteCandidate_InvalidCandidateId_ArgumentException()
        {
            // Arrange
            int candidateId = 1;
            _mockUnitOfWork.Setup(u => u.Votes.GetAll()).Returns((IEnumerable<Vote>)null);
            _mockUnitOfWork.Setup(u => u.Candidates.Get(candidateId)).Returns((Candidate)null);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _candidateService.DeleteCandidate(candidateId));
            _mockUnitOfWork.Verify(u => u.Candidates.Delete(candidateId), Times.Never);
            _mockUnitOfWork.Verify(u => u.Save(), Times.Never);
        }
    }
}
