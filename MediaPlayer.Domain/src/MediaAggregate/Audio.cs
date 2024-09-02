namespace MediaPlayer.Domain.src.MediaAggregate
{
    public class Audio : Media
    {
        public string SoundEffect { get; set; }
        public int Volume { get; set; }
    }
}