namespace SGLNU.BLL.DTO
{
    public class EventDTO
    {
        public EventDTO() { }
        public EventDTO(int id, string name, string description, string formOfHolding,
            string location, string profilePhotoFilePath, string eventType, int facultyId)
        {
            Id = id;
            Name = name;
            Description = description;
            FormOfHolding = formOfHolding;
            Location = location;
            ProfilePhotoFilePath = profilePhotoFilePath;
            EventType = eventType;
            FacultyId = facultyId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FormOfHolding { get; set; }
        public string Location { get; set; }
        public string ProfilePhotoFilePath { get; set; }
        public string EventType { get; set; }
        public int FacultyId { get; set; }
    }
}