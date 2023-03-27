using System.Collections.Generic;
using SGLNU.BLL.DTO;

namespace SGLNU.Web.ViewModels
{
    public class VotingViewModel
    {
        public IEnumerable<VotingDTO> Votings { get; set; }
    }
}
