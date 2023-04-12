using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.BLL.Interfaces;
using SGLNU.BLL.Services;
using SGLNU.DAL.Entities;
using SGLNU.DAL.Interfaces;
using NUnit;
using Moq;
using Microsoft.AspNetCore.Routing.Matching;

namespace SGLNU.Tests;

public class VotingServiceTests
{
    private VotingService _votingService;
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<IMapper> _mockMapper;
    private IMapper _mapper;

    [SetUp]
    public void SetUp()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _votingService = new VotingService(_mockUnitOfWork.Object, _mockMapper.Object);
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Voting, VotingDTO>();
            cfg.CreateMap<VotingDTO, Voting>();
            cfg.CreateMap<FacultyDTO, Faculty>();
            cfg.CreateMap<Faculty, FacultyDTO>();
            cfg.CreateMap<CandidateDTO, Candidate>();
            cfg.CreateMap<Candidate, CandidateDTO>();
            cfg.CreateMap<VoteDTO, Vote>();
            cfg.CreateMap<Vote, VoteDTO>();
        });
        _mapper = config.CreateMapper();
    }

    [Test]
    public void GetAllVotings_ShouldReturnCorrectVotings()
    {
        // Arrange
        var votingEntities = new List<Voting>
        {
            new Voting { Id = 1, Title = "Voting 1" },
            new Voting { Id = 2, Title = "Voting 2" },
            new Voting { Id = 3,  Title = "Voting 3" }
        };
        var expectedVotingDTOs = new List<VotingDTO>
        {
            new VotingDTO { Id = 1, Title = "Voting 1" },
            new VotingDTO { Id = 2, Title = "Voting 2" },
            new VotingDTO { Id = 3, Title = "Voting 3" }
        };
        _mockUnitOfWork.Setup(uow => uow.Votings.GetAll()).Returns(votingEntities);
        _mockMapper.Setup(m => m.Map<IEnumerable<Voting>, IEnumerable<VotingDTO>>(votingEntities))
                   .Returns(expectedVotingDTOs);

        // Act
        var result = _votingService.GetAllVotings();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedVotingDTOs.Count, result.Count());
        Assert.IsTrue(expectedVotingDTOs.SequenceEqual(result));
    }

    [Test]
    public void GetAllActiveVotings_ReturnsOnlyActiveVotings()
    {
        // Arrange
        var activeVotings = new List<Voting>
        {
            new Voting { Id = 1, IsActive = true },
            new Voting { Id = 2, IsActive = true }
        };
        var inactiveVotings = new List<Voting>
        {
            new Voting { Id = 3, IsActive = false }
        };
        var expectedVotings = new List<VotingDTO>
        {
            new VotingDTO { Id = 1 },
            new VotingDTO { Id = 2 }
        };
        _mockUnitOfWork
            .Setup(uow => uow.Votings.GetAll())
            .Returns(activeVotings.Concat(inactiveVotings));
        _mockMapper
            .Setup(mapper => mapper.Map<IEnumerable<Voting>, List<VotingDTO>>(activeVotings))
            .Returns(expectedVotings);

        // Act
        var result = _votingService.GetAllActiveVotings();

        // Assert
        CollectionAssert.AreEqual(expectedVotings, result.ToList());
    }

    [Test]
    public void GetFutureVotings_ReturnsOnlyFutureVotings()
    {
        // Arrange
        var futureVotings = new List<Voting>
        {
            new Voting { Id = 1, StartDate = DateTime.Today.AddDays(1) },
            new Voting { Id = 2, StartDate = DateTime.Today.AddDays(2) }
        };
        var pastVotings = new List<Voting>
        {
            new Voting { Id = 3, StartDate = DateTime.Today.AddDays(-1) }
        };
        var expectedVotings = new List<VotingDTO>
        {
            new VotingDTO { Id = 1 },
            new VotingDTO { Id = 2 }
        };
        _mockUnitOfWork
            .Setup(uow => uow.Votings.GetAll())
            .Returns(futureVotings.Concat(pastVotings));
        _mockMapper
            .Setup(mapper => mapper.Map<IEnumerable<Voting>, List<VotingDTO>>(futureVotings))
            .Returns(expectedVotings);

        // Act
        var result = _votingService.GetFutureVotings();

        // Assert
        CollectionAssert.AreEqual(expectedVotings, result.ToList());
    }

    [Test]
    public void CreateVoting_WithValidInput_ShouldReturnCreatedVoting()
    {
        // Arrange
        var service = new VotingService(_mockUnitOfWork.Object, _mapper);
        var input = new VotingDTO
        {
            Id = 1,
            Title = "Voting1",
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(1),
            Faculty = new FacultyDTO { Id = 1, Name= "Faculty1", UniversityId=1 },
            FacultyId = 1,
            IsActive = false
        };
        var voting = _mapper.Map<Voting>(input);
        var createdVoting = new Voting {
            Id = 1,
            Title = "Voting1",
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(1),
            FacultyId = 1,
            IsActive = false
        };
        _mockUnitOfWork.Setup(uow => uow.Votings.Create(It.IsAny<Voting>()))
            .Returns(createdVoting);

        // Act
        var result = service.CreateVoting(input);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(createdVoting.Id, result.Id);
    }

    [Test]
    public void CreateVoting_WithNullInput_ShouldThrowArgumentNullException()
    {
        // Arrange
        var service = new VotingService(_mockUnitOfWork.Object, _mapper);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => service.CreateVoting(null));
    }

    [Test]
    public void GetVoting_WithValidId_ReturnsVotingDTO()
    {
        // Arrange
        int votingId = 1;
        Voting voting = new Voting { Id = votingId, Title = "Test Voting" };
        _mockUnitOfWork.Setup(uow => uow.Votings.Get(votingId)).Returns(voting);
        VotingService votingService = new VotingService(_mockUnitOfWork.Object, _mapper);

        // Act
        VotingDTO result = votingService.GetVoting(votingId);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(votingId, result.Id);
        Assert.AreEqual("Test Voting", result.Title);
    }

    [Test]
    public void GetVoting_WithInvalidId_ReturnsNull()
    {
        // Arrange
        int votingId = 2;
        _mockUnitOfWork.Setup(uow => uow.Votings.Get(votingId)).Returns((Voting)null);
        VotingService votingService = new VotingService(_mockUnitOfWork.Object, _mapper);

        // Act
        VotingDTO result = votingService.GetVoting(votingId);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void UpdateVoting_WithValidVotingDTO_ShouldUpdateVotingEntity()
    {
        // Arrange
        var votingDTO = new VotingDTO
        {
            Id = 1,
            Title = "Test Voting",
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(7),
            FacultyId = 1
        };

        var votingEntity = new Voting
        {
            Id = 1,
            Title = "Test Voting",
            StartDate = DateTime.Now.AddDays(-1),
            EndDate = DateTime.Now.AddDays(1),
            FacultyId = 1
        };

        _mockUnitOfWork.Setup(u => u.Votings.Get(votingDTO.Id)).Returns(votingEntity);

        var service = new VotingService(_mockUnitOfWork.Object, _mapper);

        // Act
        service.UpdateVoting(votingDTO);

        // Assert
        _mockUnitOfWork.Verify(u => u.Votings.Update(It.IsAny<Voting>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.Save(), Times.Once);

        Assert.AreEqual(votingDTO.Title, votingEntity.Title);
        Assert.AreEqual(votingDTO.StartDate, votingEntity.StartDate);
        Assert.AreEqual(votingDTO.EndDate, votingEntity.EndDate);
        Assert.AreEqual(votingDTO.FacultyId, votingEntity.FacultyId);
    }

    [Test]
    public void UpdateVoting_WithNullVotingDTO_ShouldThrowArgumentNullException()
    {
        // Arrange
        VotingDTO votingDTO = null;

        var service = new VotingService(_mockUnitOfWork.Object, _mapper);

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => service.UpdateVoting(votingDTO));
    }

    [Test]
    public void ActivateVoting_WithCandidates_ActivatesVoting()
    {
        // Arrange
        var votingEntity = new Voting
        {
            Id = 1,
            Candidates = new List<Candidate>
            {
                new Candidate(),
                new Candidate()
            }
        };
        _mockUnitOfWork.Setup(uow => uow.Votings.Get(1)).Returns(votingEntity);
        var votingService = new VotingService(_mockUnitOfWork.Object, _mapper);

        // Act
        votingService.ActivateVoting(1);

        // Assert
        Assert.IsTrue(votingEntity.IsActive);
        _mockUnitOfWork.Verify(uow => uow.Votings.Update(votingEntity), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
    }

    [Test]
    public void ActivateVoting_WithNoCandidates_DoesNotActivateVoting()
    {
        // Arrange
        var votingEntity = new Voting
        {
            Id = 1,
            Candidates = new List<Candidate>()
        };
        _mockUnitOfWork.Setup(uow => uow.Votings.Get(1)).Returns(votingEntity);
        var votingService = new VotingService(_mockUnitOfWork.Object, _mapper);

        // Act
        votingService.ActivateVoting(1);

        // Assert
        Assert.IsFalse(votingEntity.IsActive);
        _mockUnitOfWork.Verify(uow => uow.Votings.Update(votingEntity), Times.Never);
        _mockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
    }

    [Test]
    public void DeActivateVoting_ShouldDeactivateVoting()
    {
        // Arrange
        var votingId = 1;
        var votingEntity = new Voting { Id = votingId, IsActive = true };
        _mockUnitOfWork.Setup(uow => uow.Votings.Get(votingId)).Returns(votingEntity);
        var votingService = new VotingService(_mockUnitOfWork.Object, _mapper);

        // Act
        votingService.DeActivateVoting(votingId);

        // Assert
        Assert.IsFalse(votingEntity.IsActive);
        _mockUnitOfWork.Verify(uow => uow.Votings.Update(votingEntity), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
    }

    [Test]
    public void AddCandidate_Should_AddCandidateToVoting()
    {
        // Arrange
        var votingId = 1;
        var votingEntity = new Voting
        {
            Id = votingId,
            Candidates = new List<Candidate>()
        };
        _mockUnitOfWork.Setup(uow => uow.Votings.Get(votingId)).Returns(votingEntity);

        var votingCandidateDTO = new CandidateDTO
        {
            Email = "test@test.com",
            FirstName = "Test",
            LastName = "Candidate",
            Id = 0,
            VotingId = 1
        };
        var candidateEntity = new Candidate
        {
            Email = votingCandidateDTO.Email,
            FirstName = votingCandidateDTO.FirstName,
            LastName = votingCandidateDTO.LastName
        };
        _mockUnitOfWork.Setup(uow => uow.Candidates.Create(It.IsAny<Candidate>())).Returns(candidateEntity);

        var votingService = new VotingService(_mockUnitOfWork.Object, _mapper);

        // Act
        var result = votingService.AddCandidate(votingCandidateDTO);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(votingId, result.Id);
        Assert.AreEqual(1, result.Candidates.Count);
        Assert.AreEqual(votingCandidateDTO.Email.ToLower(), result.Candidates.First().Email.ToLower());
        _mockUnitOfWork.Verify(uow => uow.Votings.Get(votingId), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.Candidates.Create(It.IsAny<Candidate>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.Votings.Update(votingEntity), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
    }

    [Test]
    public void RemoveCandidate_ReturnsExpectedResult()
    {
        // Arrange
        var votingId = 1;
        var candidateId = 2;
        var inactiveVoting = new Voting { Id = votingId, IsActive = false, Candidates = new List<Candidate>() };
        var candidate = new Candidate { Id = candidateId, VotingId = votingId, Votes = new List<Vote>() };
        inactiveVoting.Candidates.Add(candidate);
        _mockUnitOfWork.Setup(uow => uow.Votings.Get(votingId)).Returns(inactiveVoting);
        _mockUnitOfWork.Setup(uow => uow.Candidates.Get(candidateId)).Returns(candidate);
        var service = new VotingService(_mockUnitOfWork.Object, _mapper);

        // Act
        var result = service.RemoveCandidate(candidateId);

        // Assert
        Assert.AreEqual(votingId, result.Id);
        Assert.IsFalse(result.IsActive);
        _mockUnitOfWork.Verify(uow => uow.Candidates.Delete(candidateId), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.Votes.Delete(It.IsAny<int>()), Times.Exactly(candidate.Votes.Count));
        _mockUnitOfWork.Verify(uow => uow.Votings.Update(inactiveVoting), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
    }

    [Test]
    public void AddVote_WithValidData_ShouldAddNewVote()
    {
        // Arrange
        var votingId = 1;
        var candidateId = 2;
        var userEmail = "test@test.com";
        var candidateEntity = new Candidate
        {
            Id = candidateId,
            FirstName = "John",
            LastName = "Smith",
            ProgramShort = "A candidate",
            Votes = new List<Vote>(),
            Email = "asdasd@sfd.com",
            VotingId = votingId
        };
        var votingEntity = new Voting
        {
            Id = votingId,
            IsActive = true,
            StartDate = DateTime.Now.AddDays(-1),
            EndDate = DateTime.Now.AddDays(1),
            Candidates = new List<Candidate>(),
            FacultyId = 1
        };
        var VoteEntity = new Vote
        {
            Id = votingId,
            AuthorEmail = userEmail,
            CandidateId = candidateId
        };
        votingEntity.Candidates.Add(candidateEntity);
        candidateEntity.Votes.Add(VoteEntity);
        _mockUnitOfWork.Setup(u => u.Votings.Get(votingId)).Returns(votingEntity);
        _mockUnitOfWork.Setup(u => u.Candidates.Get(candidateId)).Returns(candidateEntity);

        var service = new VotingService(_mockUnitOfWork.Object, _mapper);

        // Act
        var result = service.AddVote(votingId, candidateId, userEmail);

        // Assert
        Assert.IsNotNull(result);
    }

    [Test]
    public void DeleteVoting_ValidVotingId_VotingDeleted()
    {
        // Arrange
        int votingId = 1;
        var votingEntity = new Voting { Id = votingId };
        _mockUnitOfWork.Setup(u => u.Votings.Get(votingId)).Returns(votingEntity);

        // Act
        _votingService.DeleteVoting(votingId);

        // Assert
        _mockUnitOfWork.Verify(u => u.Votings.Delete(votingId), Times.Once);
        _mockUnitOfWork.Verify(u => u.Save(), Times.Once);
    }

    [Test]
    public void DeleteVoting_InvalidVotingId_ThrowsException()
    {
        // Arrange
        int votingId = 1;
        _mockUnitOfWork.Setup(u => u.Votings.Get(votingId)).Returns((Voting)null);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _votingService.DeleteVoting(votingId));
        _mockUnitOfWork.Verify(u => u.Votings.Delete(votingId), Times.Never);
        _mockUnitOfWork.Verify(u => u.Save(), Times.Never);
    }
}
