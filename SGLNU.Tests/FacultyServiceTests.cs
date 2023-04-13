using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using SGLNU.BLL.DTO;
using SGLNU.BLL.Services;
using SGLNU.DAL.Entities;
using SGLNU.DAL.Interfaces;

namespace SGLNU.Tests
{
    public class FacultyServiceTests
    {
        private FacultyService _facultyService;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IMapper> _mockMapper;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _facultyService = new FacultyService(_mockUnitOfWork.Object, _mockMapper.Object);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FacultyDTO, Faculty>();
                cfg.CreateMap<Faculty, FacultyDTO>();
            });
            _mapper = config.CreateMapper();
        }

        [Test]
        public void GetAllFaculties_ShouldReturnCorrectFaculties()
        {
            // Arrange
            var facultyEntities = new List<Faculty>
            {
                new() { Id = 1, Name = "Name 1" },
                new() { Id = 2, Name = "Name 2" },
                new() { Id = 3, Name = "Name 3" }
            };
            var expectedFacultyDTOs = new List<FacultyDTO>
            {
                new() { Id = 1, Name = "Name 1" },
                new() { Id = 2, Name = "Name 2" },
                new() { Id = 3, Name = "Name 3" }
            };
            _mockUnitOfWork.Setup(uow => uow.Faculties.GetAll()).Returns(facultyEntities);
            _mockMapper.Setup(m => m.Map<IEnumerable<Faculty>, IEnumerable<FacultyDTO>>(facultyEntities))
                .Returns(expectedFacultyDTOs);

            // Act
            var result = _facultyService.GetAllFaculties();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedFacultyDTOs.Count, result.Count());
            Assert.IsTrue(expectedFacultyDTOs.SequenceEqual(result));
        }

        [Test]
        public void GetFaculties_WithNoFacultiesFound_ShouldThrowNullReferenceException()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.Faculties.GetAll()).Returns((IEnumerable<Faculty>)null);

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => _facultyService.GetAllFaculties());
        }

        [Test]
        public void GetAllFacultyNames_ShouldReturnCorrectFacultyNames()
        {
            // Arrange
            var facultyEntities = new List<Faculty>
            {
                new() { Id = 1, Name = "Name 1" },
                new() { Id = 2, Name = "Name 2" },
                new() { Id = 3, Name = "Name 3" }
            };
            var expectedFacultyNames = new List<string>
            {
                "Name 1",
                "Name 2",
                "Name 3"
            };
            _mockUnitOfWork.Setup(uow => uow.Faculties.GetAll()).Returns(facultyEntities);

            // Act
            var result = _facultyService.GetAllFacultiesNames();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedFacultyNames.Count, result.Count());
            Assert.IsTrue(expectedFacultyNames.SequenceEqual(result));
        }

        [Test]
        public void GetAllFacultyNames_WithNoFacultiesFound_ShouldThrowNullReferenceException()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.Faculties.GetAll()).Returns((IEnumerable<Faculty>)null);

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => _facultyService.GetAllFacultiesNames());
        }

        [Test]
        public void GetFacultyIdByName_ShouldReturnCorrectFacultyId()
        {
            var facultyName = "Name 1";
            // Arrange
            var facultyEntities = new List<Faculty>
            {
                new() { Id = 1, Name = "Name 1" },
                new() { Id = 2, Name = "Name 2" },
                new() { Id = 3, Name = "Name 3" }
            };
            var expectedFacultyId = facultyEntities.FirstOrDefault(f => f.Name == facultyName).Id;
            _mockUnitOfWork.Setup(uow => uow.Faculties.GetAll()).Returns(facultyEntities);

            // Act
            var result = _facultyService.GetFacultyIdByName(facultyName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedFacultyId, result);
        }

        [Test]
        public void GetAllFacultyIdByName_WithNoFacultiesFound_ShouldThrowArgumentNullException()
        {
            var facultyName = "Name 1";

            // Arrange
            _mockUnitOfWork.Setup(uow => uow.Faculties.GetAll()).Returns((IEnumerable<Faculty>)null);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _facultyService.GetFacultyIdByName(facultyName));
        }

        [Test]
        public void GetAllFacultyIdByName_WithFacultyNotFound_ShouldThrowNullReferenceException()
        {
            var facultyName = "Name 0";
            
            // Arrange
            var facultyEntities = new List<Faculty>
            {
                new() { Id = 1, Name = "Name 1" },
                new() { Id = 2, Name = "Name 2" },
                new() { Id = 3, Name = "Name 3" }
            };
            var expectedFacultyDTOs = new List<FacultyDTO>
            {
                new() { Id = 1, Name = "Name 1" },
                new() { Id = 2, Name = "Name 2" },
                new() { Id = 3, Name = "Name 3" }
            };
            _mockUnitOfWork.Setup(uow => uow.Faculties.GetAll()).Returns(facultyEntities);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _facultyService.GetFacultyIdByName(facultyName));
        }
    }
}
