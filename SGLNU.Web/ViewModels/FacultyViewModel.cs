namespace SGLNU.Web.ViewModels
{
    public class FacultyViewModel
    {
        public FacultyViewModel() { }
        public FacultyViewModel(string name, int id)
        {
            this.FacultyId = id;
            this.Name = name;
        }
        public int FacultyId { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}
