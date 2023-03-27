using System.ComponentModel.DataAnnotations;

namespace SGLNU.DAL.Entities
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string DocumentFilePath { get; set; }
        public string DocumentType { get; set; }
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
    }
}
