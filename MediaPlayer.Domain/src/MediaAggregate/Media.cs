namespace MediaPlayer.Domain.src.MediaAggregate
{
    public abstract class Media
    {
        public Guid MediaId { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }

        public bool IsPlaying { get; set; }

        public bool IsPaused { get; set; }

        public bool IsStopped { get; set; }
    }
}