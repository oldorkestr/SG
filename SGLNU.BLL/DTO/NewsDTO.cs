using System;
using System.Collections.Generic;
using System.Text;

namespace SGLNU.BLL.DTO
{
    public class NewsDTO
    {
        public NewsDTO() { }
        public NewsDTO(string id, string tilte, string description, string photoFilePath, int facultyId, DateTime date, int likes)
        {
            Id = id;
            Tilte = tilte;
            Description = description;
            PhotoFilePath = photoFilePath;
            FacultyId = facultyId;
            CreationDate = date;
            NumberOflikes = likes;
        }

        public string Id { get; set; }
        public string Tilte { get; set; }
        public string Description { get; set; }
        public string PhotoFilePath { get; set; }
        public int NumberOflikes { get; set; }
        public DateTime CreationDate { get; set; }
        public int FacultyId { get; set; }
    }
}