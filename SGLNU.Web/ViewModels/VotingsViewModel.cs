using System.Collections.Generic;
using SGLNU.BLL.DTO;

namespace SGLNU.Web.ViewModels
{
    public class VotingsViewModel
    {
        public IEnumerable<VotingDTO> Votings { get; set; }
    }
}
