namespace MediaPlayer.Domain.src.UserAggregate
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<PlayList> PlayLists { get; set; } = new List<PlayList>();
    }
}