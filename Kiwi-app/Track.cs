using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwi_app
{
    /// <summary>
    /// Represents a track/song in the system.
    /// </summary>
    /// <param name="Title">The track's title.</param>
    /// <param name="Artist">The artist associated with the track.</param>
    /// <param name="Album">The album associated with the track.</param>
    /// <param name="Duration">The track's length.</param>
    /// <param name="TrackGenres">A list of one of more genres that may be associated with a track.</param>
    internal class Track(string Title, Artist Artist, Album Album, TimeSpan Duration, List<Genre> TrackGenres)
    {
        public string Title { get; } = Title ?? throw new ArgumentNullException(nameof(Title), "Track title cannot be null");
        public Artist Artist { get; } = Artist ?? throw new ArgumentNullException(nameof(Artist), "Artist cannot be null");
        public Album Album { get; } = Album ?? throw new ArgumentNullException(nameof(Album), "Album cannot be null");
        public TimeSpan Duration { get; } = Duration;
        public List<Genre> TrackGenres { get; } = TrackGenres ?? throw new ArgumentNullException(nameof(TrackGenres), "Genres cannot be null");
        public List<double> TrackRatings { get; } = new();
        public double AverageRating => TrackRatings.Count == 0 ? 0 : TrackRatings.Average();

        public string TrackInfo =>
            $"Song Info:\n" +
            $"Title: {Title}\n" +
            $"Artist: {Artist.Name}\n" +
            $"Album: {Album.Title}\n" +
            $"Genre: {string.Join(", ", TrackGenres)}\n)" +
            $"Length: {Duration}";
    }
}
