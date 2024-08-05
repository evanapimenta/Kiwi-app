using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwi_app
{
    /// <summary>
    /// Represents an episode in the system, that may be linked to a Podcast.
    /// </summary>
    /// <param name="EpisodeTitle">The name of the podcast's episode.</param>
    /// <param name="Podcast">The podcast linked to the episode.</param>
    internal class Episode(string EpisodeTitle, Podcast Podcast)
    {
        public Podcast Podcast { get; } = Podcast ?? throw new ArgumentNullException(nameof(Podcast), "Podcast cannot be null");
        public string EpisodeTitle { get; } = EpisodeTitle ?? throw new ArgumentNullException(nameof(EpisodeTitle), "Episode title cannot be null");
        public TimeSpan EpisodeDuration { get; set; }
        public int EpisodeNumber { get; } = Podcast.Episodes.Count + 1;
        public HashSet<string> Guests { get; } = new();

        public List<int> EpisodeRatings = new();
        public double AverageRating => EpisodeRatings.Average();

        public string EpisodeInfo => $"{EpisodeNumber}. {EpisodeTitle} ({EpisodeDuration})\n" +
            $"Guests: {string.Join(", ", Guests)}" +
            $"Episode Rating: {AverageRating} ({EpisodeRatings.Count} ratings)";


        /// <summary>
        /// Adds a guest to an episode.
        /// </summary>
        /// <param name="guestName">The name of the guest to be added to an episode.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="guestName"/> or <paramref name="Guests"/> is null.</exception>
        public static void AddGuestToEpisode(string guestName, HashSet<string> Guests)
        {
            if (guestName == null) throw new ArgumentNullException(nameof(guestName), "Guest name cannot be null");
            if (Guests == null) throw new ArgumentNullException(nameof(Guests), "List of guests cannot be null");
            
            Guests.Add(guestName); 
        }
    }
}
