using AutoMapper;
using Microsoft.Extensions.Logging;
using SGLNU.BLL.DTO;
using SGLNU.BLL.Interfaces;
using SGLNU.DAL.Entities;
using SGLNU.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGLNU.BLL.Services
{
    public class FacultyService : IFacultyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FacultyService() { }

        public FacultyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public FacultyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<string> GetAllFacultiesNames()
        {
            var faculties = _unitOfWork.Faculties.GetAll();
            if (faculties != null)
            {
                return faculties.Select(f => f.Name);
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public IEnumerable<FacultyDTO> GetAllFaculties()
        {
            var faculties = _unitOfWork.Faculties.GetAll();
            if (faculties != null)
            {
                return _mapper.Map<IEnumerable<Faculty>, IEnumerable<FacultyDTO>>(faculties);
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public int GetFacultyIdByName(string facultyName)
        {
            var allFaculties = _unitOfWork.Faculties.GetAll();
            var faculty = allFaculties.FirstOrDefault(f => f.Name == facultyName);
            if (faculty != null)
            {
                return faculty.Id;
            }
            else
            {
                throw new ArgumentException(nameof(facultyName));
            }
        }

        public string GetFacultyNameById(int facultyId)
        {
            var faculty = _unitOfWork.Faculties.Get(facultyId);
            if (faculty != null)
            {
                return faculty.Name;
            }
            else
            {
                throw new ArgumentException(nameof(facultyId));
            }

        }
    }
}
