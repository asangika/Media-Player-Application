using MediaPlayer.Domain.src.MediaAggregate;
using MediaPlayer.Service.src.Services;

namespace MediaPlayer.Tests
{
    public class VideoServiceTests
    {
        private readonly VideoService _videoService;
        private readonly List<Video> _videoCollection;

        public VideoServiceTests()
        {
            _videoCollection = new List<Video>();
            _videoService = new VideoService(_videoCollection);
        }

        [Fact]
        public void AddMedia_ShouldAddvideoToCollection()
        {
            // Arrange
            var video = new Video { MediaId = Guid.NewGuid(), Name = "Test video", Duration = TimeSpan.FromMinutes(3) };

            // Act
            _videoService.AddMedia(video);

            // Assert
            Assert.Contains(video, _videoCollection);
        }

        [Fact]
        public void GetMediaByName_ShouldReturnCorrectvideo()
        {
            // Arrange
            var video = new Video { MediaId = Guid.NewGuid(), Name = "Test video", Duration = TimeSpan.FromMinutes(3) };
            _videoCollection.Add(video);

            // Act
            var result = _videoService.GetMediaByName("Test video");

            // Assert
            Assert.Equal(video, result);
        }

        [Fact]
        public void GetMediaByName_ShouldReturnNull_IfNameDoesNotExist()
        {
            // Act
            var result = _videoService.GetMediaByName("Nonexistent video");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetMediaById_ShouldReturnCorrectvideo()
        {
            // Arrange
            var video = new Video { MediaId = Guid.NewGuid(), Name = "Test video", Duration = TimeSpan.FromMinutes(3) };
            _videoCollection.Add(video);

            // Act
            var result = _videoService.GetMediaById(video.MediaId);

            // Assert
            Assert.Equal(video, result);
        }

        [Fact]
        public void GetMediaById_ShouldReturnNull_IfIdDoesNotExist()
        {
            // Act
            var result = _videoService.GetMediaById(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void RemoveMedia_ShouldRemovevideoFromCollection()
        {
            // Arrange
            var video = new Video { MediaId = Guid.NewGuid(), Name = "Test video", Duration = TimeSpan.FromMinutes(3) };
            _videoCollection.Add(video);

            // Act
            _videoService.RemoveMedia(video.MediaId);

            // Assert
            Assert.DoesNotContain(video, _videoCollection);
        }

        [Fact]
        public void RemoveMedia_ShouldDoNothing_IfIdDoesNotExist()
        {
            // Arrange
            var initialCount = _videoCollection.Count;

            // Act
            _videoService.RemoveMedia(Guid.NewGuid());

            // Assert
            Assert.Equal(initialCount, _videoCollection.Count);
        }

        [Fact]
        public void UpdateMedia_ShouldUpdateExistingvideo()
        {
            // Arrange
            var video = new Video
            {
                MediaId = Guid.NewGuid(),
                Name = "Test video",
                Duration = TimeSpan.FromMinutes(3),
                Volume = 50,
                Brightness = 5
            };
            _videoCollection.Add(video);

            var updatedvideo = new Video
            {
                MediaId = video.MediaId,
                Name = "Updated video",
                Duration = TimeSpan.FromMinutes(4),
                Volume = 80,
                Brightness = 7
            };

            // Act
            _videoService.UpdateMedia(updatedvideo);

            // Assert
            var result = _videoService.GetMediaById(video.MediaId);
            Assert.Equal("Updated video", result.Name);
            Assert.Equal(TimeSpan.FromMinutes(4), result.Duration);
            Assert.Equal(80, result.Volume);
            Assert.Equal(7, result.Brightness);
        }

        [Fact]
        public void UpdateMedia_ShouldDoNothing_IfvideoDoesNotExist()
        {
            // Arrange
            var updatedvideo = new Video
            {
                MediaId = Guid.NewGuid(),
                Name = "Nonexistent video",
                Duration = TimeSpan.FromMinutes(4),
                Volume = 80,
                Brightness = 7
            };

            // Act
            _videoService.UpdateMedia(updatedvideo);

            // Assert
            var result = _videoService.GetMediaById(updatedvideo.MediaId);
            Assert.Null(result);
        }
    }
}
