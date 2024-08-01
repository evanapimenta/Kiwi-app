using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwi_app
{
    /// <summary>
    /// Represents a playlist in the system that may be associated with a user.
    /// </summary>
    /// <param name="User">The user that the playlist will be linked to.</param>
    internal class Playlist(User User)
    {
        private static readonly HashSet<int> userPlaylistIds = new();
        private static readonly Random random = new();
        private static readonly object lockObject = new();


        private TimeSpan _playlistDuration;
        public User User { get; } = User ?? throw new ArgumentNullException(nameof(User), "User cannot be null");
        public string PlaylistName { get; } = $"Playlist nº {(User.PlaylistCount + 1)}";
        public int PlaylistId { get; } = GenerateUniquePlaylistId();

        public bool IsEmpty { get; private set; } = true;
        public List<Track> PlaylistTracks { get; } = new();
        public TimeSpan PlaylistDuration {
            get => _playlistDuration;
            private set => _playlistDuration = value;
        }

        /// <summary>
        /// Updates the total duration of the playlist by summing the lengths of all tracks.
        /// </summary>
        public void UpdatePlaylistDuration()
        {
            _playlistDuration = TimeSpan.FromHours(PlaylistTracks.Sum(track => track.Duration.TotalHours));
        }

        /// <summary>
        /// Generates an unique ID to the playlist.
        /// </summary>
        /// <returns>A unique integer value that will represent a certain user playlist.</returns>
        public static int GenerateUniquePlaylistId()
        {
            int newPlaylistId;
            Random random = new();

            lock (lockObject)
            {
                do
                {
                    newPlaylistId = random.Next(0, 1500000);

                } while (!Playlist.AddPlaylistId(newPlaylistId));
            }

            return newPlaylistId;
        }

        public static bool AddPlaylistId(int playlistId)
        {
            return userPlaylistIds.Add(playlistId);
        }
    }
}
