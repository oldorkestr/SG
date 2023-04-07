using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGLNU.DAL.Entities
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] Photo { get; set; }

        public string ProgramShort { get; set; }

        public string ProgramExtended { get; set; }

        public int VotingId { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
