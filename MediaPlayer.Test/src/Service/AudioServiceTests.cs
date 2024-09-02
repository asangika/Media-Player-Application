using MediaPlayer.Domain.src.MediaAggregate;
using MediaPlayer.Service.src.Services;

namespace MediaPlayer.Tests
{
    public class AudioServiceTests
    {
        private readonly AudioService _audioService;
        private readonly List<Audio> _audioCollection;

        public AudioServiceTests()
        {
            _audioCollection = new List<Audio>();
            _audioService = new AudioService(_audioCollection);
        }

        [Fact]
        public void AddMedia_ShouldAddAudioToCollection()
        {
            // Arrange
            var audio = new Audio { MediaId = Guid.NewGuid(), Name = "Test Audio", Duration = TimeSpan.FromMinutes(3) };

            // Act
            _audioService.AddMedia(audio);

            // Assert
            Assert.Contains(audio, _audioCollection);
        }

        [Fact]
        public void GetMediaByName_ShouldReturnCorrectAudio()
        {
            // Arrange
            var audio = new Audio { MediaId = Guid.NewGuid(), Name = "Test Audio", Duration = TimeSpan.FromMinutes(3) };
            _audioCollection.Add(audio);

            // Act
            var result = _audioService.GetMediaByName("Test Audio");

            // Assert
            Assert.Equal(audio, result);
        }

        [Fact]
        public void GetMediaByName_ShouldReturnNull_IfNameDoesNotExist()
        {
            // Act
            var result = _audioService.GetMediaByName("Nonexistent Audio");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetMediaById_ShouldReturnCorrectAudio()
        {
            // Arrange
            var audio = new Audio { MediaId = Guid.NewGuid(), Name = "Test Audio", Duration = TimeSpan.FromMinutes(3) };
            _audioCollection.Add(audio);

            // Act
            var result = _audioService.GetMediaById(audio.MediaId);

            // Assert
            Assert.Equal(audio, result);
        }

        [Fact]
        public void GetMediaById_ShouldReturnNull_IfIdDoesNotExist()
        {
            // Act
            var result = _audioService.GetMediaById(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void RemoveMedia_ShouldRemoveAudioFromCollection()
        {
            // Arrange
            var audio = new Audio { MediaId = Guid.NewGuid(), Name = "Test Audio", Duration = TimeSpan.FromMinutes(3) };
            _audioCollection.Add(audio);

            // Act
            _audioService.RemoveMedia(audio.MediaId);

            // Assert
            Assert.DoesNotContain(audio, _audioCollection);
        }

        [Fact]
        public void RemoveMedia_ShouldDoNothing_IfIdDoesNotExist()
        {
            // Arrange
            var initialCount = _audioCollection.Count;

            // Act
            _audioService.RemoveMedia(Guid.NewGuid());

            // Assert
            Assert.Equal(initialCount, _audioCollection.Count);
        }

        [Fact]
        public void UpdateMedia_ShouldUpdateExistingAudio()
        {
            // Arrange
            var audio = new Audio
            {
                MediaId = Guid.NewGuid(),
                Name = "Test Audio",
                Duration = TimeSpan.FromMinutes(3),
                Volume = 50,
                SoundEffect = "None"
            };
            _audioCollection.Add(audio);

            var updatedAudio = new Audio
            {
                MediaId = audio.MediaId,
                Name = "Updated Audio",
                Duration = TimeSpan.FromMinutes(4),
                Volume = 80,
                SoundEffect = "Bass Boost"
            };

            // Act
            _audioService.UpdateMedia(updatedAudio);

            // Assert
            var result = _audioService.GetMediaById(audio.MediaId);
            Assert.Equal("Updated Audio", result.Name);
            Assert.Equal(TimeSpan.FromMinutes(4), result.Duration);
            Assert.Equal(80, result.Volume);
            Assert.Equal("Bass Boost", result.SoundEffect);
        }

        [Fact]
        public void UpdateMedia_ShouldDoNothing_IfAudioDoesNotExist()
        {
            // Arrange
            var updatedAudio = new Audio
            {
                MediaId = Guid.NewGuid(),
                Name = "Nonexistent Audio",
                Duration = TimeSpan.FromMinutes(4),
                Volume = 80,
                SoundEffect = "Bass Boost"
            };

            // Act
            _audioService.UpdateMedia(updatedAudio);

            // Assert
            var result = _audioService.GetMediaById(updatedAudio.MediaId);
            Assert.Null(result);
        }
    }
}
