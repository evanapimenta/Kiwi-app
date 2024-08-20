using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwi_app.Models.Music;

/// <summary>
/// Represents a track/song in the system.
/// </summary>
/// <param name="Title">The track's title.</param>
/// <param name="Artist">The artist associated with the track.</param>
/// <param name="Album">The album associated with the track.</param>
/// <param name="Duration">The track's length.</param>
/// <param name="TrackGenres">A list of one or more genres that may be associated with a track.</param>

internal class Track(string Title, TimeSpan Duration)
{
    private Album _album;
    private Artist _artist;

    public string Title { get; } = Title ?? throw new ArgumentNullException(nameof(Title), "Track title cannot be null");
    public Artist Artist
    {
        get => _artist;
        set => _artist = value ?? throw new ArgumentNullException(nameof(value));
    }
    public Album Album
    {
        get => _album;
        set => _album = value ?? throw new ArgumentNullException(nameof(value));
    }

    public TimeSpan Duration { get; } = Duration;
    public List<Genre> TrackGenres { get; } = new();
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

