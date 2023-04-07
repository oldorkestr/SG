namespace SGLNU.Web.ViewModels
{
    public class VoteViewModel
    {
        public VoteViewModel()
        {
        }

        public VoteViewModel(int id, string authorEmail, int votingId)
        {
            Id = id;
            AuthorEmail = authorEmail;
            VotingId = votingId;
        }

        public int Id { get; set; }

        public string AuthorEmail { get; set; }

        public int VotingId { get; set; }
    }
}
