using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kiwi_app.Models.Podcast.Podcast;

/// <summary>
/// Represents an episode in the system, that may be linked to a Podcast.
/// </summary>
/// <param name="EpisodeTitle">The name of the podcast's episode.</param>
/// <param name="Podcast">The podcast linked to the episode.</param>
internal class Episode(string _episodeTitle, Podcast Podcast)
{
    public Podcast Podcast { get; } = Podcast ?? throw new ArgumentNullException(nameof(Podcast), "Podcast cannot be null");

    public string EpisodeTitle
    {
        get => _episodeTitle;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value), "Name cannot be null or empty");
            }

            _episodeTitle = value;
        }
    }

    public int EpisodeNumber => GetNextEpisodeNumber();

    public TimeSpan EpisodeDuration { get; set; }

    public HashSet<string> Guests { get; } = new();

    private List<int> _episodeRating = new();
    public IReadOnlyList<int> EpisodeRating => _episodeRating.AsReadOnly();
    public double AverageRating => _episodeRating.Count == 0 ? 0 : _episodeRating.Average();

    public string EpisodeInfo => $"{EpisodeNumber}. {_episodeTitle} ({EpisodeDuration})\n" +
        $"Guests: {string.Join(", ", Guests)}" +
        $"Episode Rating: {AverageRating} ({EpisodeRating.Count} ratings)";


    /// <summary>
    /// Adds a guest to a given episode.
    /// </summary>
    /// <param name="guestName">The name of the guest to be added to an episode.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="guestName"/> or <paramref name="Guests"/> is null.</exception>
    public static void AddGuestToEpisode(string guestName, HashSet<string> Guests)
    {
        if (guestName == null) throw new ArgumentNullException(nameof(guestName), "Guest name cannot be null");
        if (Guests == null) throw new ArgumentNullException(nameof(Guests), "List of guests cannot be null");

        Guests.Add(guestName);
    }

    /// <summary>
    /// Calculates the number for the next episode based on the current count of episodes in the podcast.
    /// </summary>
    /// <returns>The number for the next episode, which is one more than the current count of episodes in the podcast.</returns>
    private int GetNextEpisodeNumber()
    {
        return Podcast.Episodes.Count + 1;
    }

    private void RateEpisode(int rating)
    {
        if (rating < 0 || rating > 0)
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 0 and 5");

        _episodeRating.Add(rating);
    }

}
