using System;
using System.ComponentModel.DataAnnotations;

namespace SGLNU.DAL.Entities
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string Tilte { get; set; }
        public string Description { get; set; }
        public string PhotoFilePath { get; set; }
        public int NumberOflikes { get; set; }
        public int FacultyId { get; set; }
        public DateTime CreationDate { get; set; }
        public Faculty Faculty { get; set; }
    }
}
