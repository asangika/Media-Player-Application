using MediaPlayer.Controller.src;
using MediaPlayer.Domain.src.MediaAggregate;
using MediaPlayer.Service.src.Services;
using Moq;

namespace MediaPlayer.Test.src.Controller
{
    public class VideoControllerTests
    {
        private readonly Mock<VideoService> _mockVideoService;
        private readonly VideoController _videoController;

        public VideoControllerTests()
        {
            _mockVideoService = new Mock<VideoService>();
            _videoController = new VideoController(_mockVideoService.Object);
        }

        [Fact]
        public void AddVideo_ShouldCallVideoServiceAddVideo()
        {
            // Arrange
            var video = new Video { MediaId = Guid.NewGuid(), Name = "Test Video" };

            // Act
            _videoController.AddVideo(video);

            // Assert
            _mockVideoService.Verify(x => x.AddMedia(video), Times.Once);
        }

        public void AddVideo_ShouldNotAddVideo_WhenNotLoggedInAsAdmin()
        {
            // Arrange
            var video = new Video { MediaId = Guid.NewGuid(), Name = "Test Video" };
            _videoController.LoggedInAccount.Role = "User";
            // Act
            _videoController.AddVideo(video);

            // Assert
            _mockVideoService.Verify(x => x.AddMedia(video), Times.Once);
        }

        [Fact]
        public void GetVideoById_ShouldCallVideoServiceGetMediaById()
        {
            // Arrange
            var videoId = Guid.NewGuid();
            _mockVideoService.Setup(x => x.GetMediaById(videoId)).Returns(new Video());

            // Act
            var result = _videoController.GetMediaById(videoId);

            // Assert
            _mockVideoService.Verify(x => x.GetMediaById(videoId), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public void RemoveAudio_ShouldRemoveAudio_WhenLoggedInAsAdmin()
        {
            // Arrange
            var videoId = Guid.NewGuid();

            // Act
            _videoController.RemoveVideo(videoId);

            // Assert
            _mockVideoService.Verify(s => s.RemoveMedia(videoId), Times.Once);
        }

        [Fact]
        public void RemoveAudio_ShouldNotRemoveAudio_WhenNotLoggedInAsAdmin()
        {
            // Arrange
            var audioId = Guid.NewGuid();
            _videoController.LoggedInAccount.Role = "User";

            // Act
            _videoController.RemoveVideo(audioId);

            // Assert
            _mockVideoService.Verify(s => s.RemoveMedia(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public void UpdateAudio_ShouldUpdateAudio_WhenLoggedInAsAdmin()
        {
            // Arrange
            var video = new Video { MediaId = Guid.NewGuid(), Name = "Sample Video" };

            // Act
            _videoController.UpdateVideo(video);

            // Assert
            _mockVideoService.Verify(s => s.UpdateMedia(video), Times.Once);
        }

        [Fact]
        public void UpdateAudio_ShouldNotUpdateAudio_WhenNotLoggedInAsAdmin()
        {
            // Arrange
            var video = new Video { MediaId = Guid.NewGuid(), Name = "Sample Video" };
            _videoController.LoggedInAccount.Role = "User";

            // Act
            _videoController.UpdateVideo(video);

            // Assert
            _mockVideoService.Verify(s => s.UpdateMedia(It.IsAny<Video>()), Times.Never);
        }
    }
}