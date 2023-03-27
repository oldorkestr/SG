using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGLNU.BLL.DTO;

namespace SGLNU.BLL.Interfaces
{
    public interface ICandidateService
    {
        IEnumerable<CandidateDTO> GetAllCandidates();
    }
}
