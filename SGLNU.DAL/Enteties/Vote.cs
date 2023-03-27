using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGLNU.DAL.Entities
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }

        public string AuthorEmail { get; set; }

        public int CandidateId { get; set; }
    }
}
