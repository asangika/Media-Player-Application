using Moq;
using MediaPlayer.Controller.src;
using MediaPlayer.Service.src.Services;
using MediaPlayer.Domain.src.MediaAggregate;
using MediaPlayer.Domain.src.UserAggregate;

namespace MediaPlayer.Test.src.Controller
{
    public class AudioControllerTests
    {
        private readonly Mock<AudioService> _mockAudioService;
        private readonly AudioController _audioController;

        public bool IsLoggedIn { get; }
        public Account LoggedInAccount { get; }

        public AudioControllerTests()
        {
            _mockAudioService = new Mock<AudioService>();
            _audioController = new AudioController(_mockAudioService.Object);

            {
                IsLoggedIn = true;
                LoggedInAccount = new Account { UserName = "admin", Role = "Admin" };
            };
        }

        [Fact]
        public void AddAudio_ShouldCallAudioServiceAddAudio()
        {
            // Arrange
            var audio = new Audio { MediaId = Guid.NewGuid(), Name = "Test Audio" };

            // Act
            _audioController.AddAudio(audio);

            // Assert
            _mockAudioService.Verify(x => x.AddMedia(audio), Times.Once);
        }

        [Fact]
        public void AddAudio_ShouldNotAddAudio_WhenNotLoggedInAsAdmin()
        {
            // Arrange
            var audio = new Audio { MediaId = Guid.NewGuid(), Name = "Test Audio" };
            _audioController.LoggedInAccount.Role = "User";
            // Act
            _audioController.AddAudio(audio);

            // Assert
            _mockAudioService.Verify(x => x.AddMedia(audio), Times.Once);
        }

        [Fact]
        public void GetAudioById_ShouldCallAudioServiceGetMediaById()
        {
            // Arrange
            var audioId = Guid.NewGuid();
            _mockAudioService.Setup(x => x.GetMediaById(audioId)).Returns(new Audio());

            // Act
            var result = _audioController.GetMediaById(audioId);

            // Assert
            _mockAudioService.Verify(x => x.GetMediaById(audioId), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public void RemoveAudio_ShouldRemoveAudio_WhenLoggedInAsAdmin()
        {
            // Arrange
            var audioId = Guid.NewGuid();

            // Act
            _audioController.RemoveAudio(audioId);

            // Assert
            _mockAudioService.Verify(s => s.RemoveMedia(audioId), Times.Once);
        }

        [Fact]
        public void RemoveAudio_ShouldNotRemoveAudio_WhenNotLoggedInAsAdmin()
        {
            // Arrange
            var audioId = Guid.NewGuid();
            _audioController.LoggedInAccount.Role = "User";

            // Act
            _audioController.RemoveAudio(audioId);

            // Assert
            _mockAudioService.Verify(s => s.RemoveMedia(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public void UpdateAudio_ShouldUpdateAudio_WhenLoggedInAsAdmin()
        {
            // Arrange
            var audio = new Audio { MediaId = Guid.NewGuid(), Name = "Sample Audio" };

            // Act
            _audioController.UpdateAudio(audio);

            // Assert
            _mockAudioService.Verify(s => s.UpdateMedia(audio), Times.Once);
        }

        [Fact]
        public void UpdateAudio_ShouldNotUpdateAudio_WhenNotLoggedInAsAdmin()
        {
            // Arrange
            var audio = new Audio { MediaId = Guid.NewGuid(), Name = "Sample Audio" };
            _audioController.LoggedInAccount.Role = "User";

            // Act
            _audioController.UpdateAudio(audio);

            // Assert
            _mockAudioService.Verify(s => s.UpdateMedia(It.IsAny<Audio>()), Times.Never);
        }
    }
}