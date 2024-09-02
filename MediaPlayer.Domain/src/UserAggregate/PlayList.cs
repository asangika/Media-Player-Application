using MediaPlayer.Domain.src.MediaAggregate;

namespace MediaPlayer.Domain.src.UserAggregate
{
    public class PlayList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public List<Media> Items { get; set; } = new List<Media>();
        public Media PlayingItem { get; set; }

    }
}