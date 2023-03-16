using AutoMapper;
using Microsoft.Extensions.Logging;
using SGLNU.BLL.DTO;
using SGLNU.BLL.Interfaces;
using SGLNU.DAL.Enteties;
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

        public FacultyService() {}

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
            return _unitOfWork.Faculties.GetAll().Select(f=>f.Name);
        }

        public int GetFacultyIdByName(string FacultyName)
        {
            var facultyId=_unitOfWork.Faculties.GetAll().Where(f => f.Name == FacultyName).Select(f => f.Id).First();
            return facultyId;
        }

        public string GetFacultyNameById(int facultyId)
        {
            var FacultyName = _unitOfWork.Faculties.Get(facultyId).Name;
            return FacultyName;
        }
    }
}
