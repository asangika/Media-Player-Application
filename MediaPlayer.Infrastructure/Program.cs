
using MediaPlayer.Controller.src;
using MediaPlayer.Domain.src.MediaAggregate;
using MediaPlayer.Service.src.Services;

namespace MediaPlayer.Infrastructure
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Initialize services
            var accountService = new AccountService();
            var audioService = new AudioService();
            var videoService = new VideoService();
            var playListService = new PlayListService();

            // Initialize controllers
            var accountController = new AccountController(accountService);
            var audioController = new AudioController(audioService);
            var videoController = new VideoController(videoService);
            var playListController = new PlayListController(playListService);

            accountController.Register("admin", "admin123", "Admin");
            accountController.Login("admin", "admin123");

            Audio audio = null;
            Video video = null;

            if (accountController.IsLoggedIn)
            {
                //create User
                accountController.CreateAccount("user1", "password1", "User");

                // Create media files
                audio = new Audio { MediaId = Guid.NewGuid(), Name = "MyAudio", Duration = TimeSpan.FromMinutes(3) };
                video = new Video { MediaId = Guid.NewGuid(), Name = "MyVideo", Duration = TimeSpan.FromMinutes(5) };

                audioController.AddAudio(audio);
                videoController.AddVideo(video);

                //update User
                var user1 = accountController.GetAccountById(accountService.GetAccountByUsername("user1").AccountId);
                user1.Password = "newPassword";
                accountController.UpdateAccount(user1);

            }

            accountController.Login("user1", "newPassword");

            if (accountController.IsLoggedIn && accountController.LoggedInAccount.Role == "User")
            {

                var user = accountController.LoggedInAccount;
                playListController.CreatePlaylist(user, "UserPlaylist");


                var audio1 = audioService.GetMediaById(audio.MediaId);
                var video1 = videoService.GetMediaById(video.MediaId);

                playListController.AddMedia(user.PlayLists[0], audio1);
                playListController.AddMedia(user.PlayLists[0], video1);

                playListController.PlayMediaById(user, user.PlayLists[0].Id, audio1.MediaId);
                Console.WriteLine($"Playing media {audio1.Name}");

                playListController.AdjustSoundEffect(user.PlayLists[0], "BassBoost");

                playListController.PlayMediaById(user, user.PlayLists[0].Id, video1.MediaId);
                Console.WriteLine($"Playing media {video1.Name}");
                playListController.AdjustBrightness(user.PlayLists[0], 10);

                playListController.PauseMedia(user, user.PlayLists[0].Id, audio1.MediaId);
                Console.WriteLine($"Pausing media {audio1.Name}");

                playListController.AdjustSoundEffect(user.PlayLists[0], "Echo");

                playListController.StopMedia(user, user.PlayLists[0].Id, audio1.MediaId);
                Console.WriteLine($"Stopping media {audio1.Name}");
            }

            foreach (var account in accountService.GetAllAccounts())
            {
                Console.WriteLine($"Account: {account.UserName}, Role: {account.Role}");
            }

        }

    }
}
