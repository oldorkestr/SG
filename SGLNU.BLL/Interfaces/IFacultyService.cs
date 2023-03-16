using SGLNU.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGLNU.BLL.Interfaces
{
    public interface IFacultyService
    {
        public IEnumerable<string> GetAllFacultiesNames();
        public int GetFacultyIdByName(string FacultyName);
        public string GetFacultyNameById(int facultyId);
    }
}
