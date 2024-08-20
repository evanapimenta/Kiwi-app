using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kiwi_app.Models.Music;

/// <summary>
/// Represents an album in the system that may have tracks associated with it.
/// </summary>
/// <param name="artist">The artist linked to an album.</param>
/// <param name="albumTitle">The album title.</param>
/// <param name="tracklist">A list of tracks that the album contains.</param>
/// <param name="releaseYear">An integer number representing the year of release.</param>
internal class Album(Artist artist, string title, int releaseYear)
{
    public Artist Artist { get; } = artist ?? throw new ArgumentNullException(nameof(Artist), "Artist cannot be null");

    public string Title
    {
        get => title;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value), "Album title cannot be null or empty");
            }

            title = value;
        }
    }

    public int ReleaseYear { get; } = releaseYear;

    private List<Track> _tracklist = new();
    public IReadOnlyList<Track> Tracklist => _tracklist.AsReadOnly();

    private List<Genre> _genres = new();
    private IReadOnlyList<Genre> Genres => _genres.AsReadOnly();

    public TimeSpan AlbumDuration => TimeSpan.FromMinutes(Tracklist.Sum(track => track.Duration.TotalMinutes));

    private readonly List<int> _albumRatings = new();
    public IReadOnlyList<int> AlbumRatings => _albumRatings.AsReadOnly();
    public double AlbumAverage => _albumRatings.Count == 0 ? 0 : _albumRatings.Average();


    /// <summary>
    /// Adds a song to an album.
    /// </summary>
    /// <param name="track">The track to be added to the album.</param>
    /// <exception cref="ArgumentNullException">Thrown when a <paramref name="track"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the <paramref name="track"/> does not belong to this album (if such a check is implemented).</exception>
    public void AddTrackToAlbum(Track track)
    {
        if (track == null)
            throw new ArgumentNullException(nameof(track), "Track cannot be null");

        _tracklist.Add(track);

    }

    /// <summary>
    /// Prints detailed information about the specified album, including its properties and tracklist.
    /// </summary>
    /// <param name="album">The album whose information is to be printed.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="album"/> is null.</exception>

    public void PrintAlbumInfo()
    {
        Console.WriteLine($"ALBUM INFO:\n" +
            $"Artist: {Artist.Name}\n" +
            $"Album: {Title}\n" +
            $"Genres: {string.Join(", ", Genres)}\n" +
            $"Length: {AlbumDuration}\n");

        PrintAlbumTracklist();
    }

    public void PrintAlbumTracklist()
    {
        var properties = GetType().GetProperties();
        foreach (var property in properties)
        {
            var value = property.GetValue(this, null);
            if (value is IList<Track> tracklist)
            {
                Console.WriteLine($"{property.Name}");

                int i = 1;
                foreach (var track in tracklist)
                {
                    if (track != null)
                    {
                        Console.WriteLine($"{i++}. {track.Title} ({track.Duration})");
                    }
                }
            }
        }
    }

    /// <summary>
    /// Rates the album with a given rating, adding it to the artist's list of ratings.
    /// </summary>
    /// <param name="rating">The rating to be added, which must be between 0 and 5 (inclusive).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="rating"/> is less than 0 or greater than 5.</exception>
    public void RateArtist(int rating)
    {
        if (rating < 0 || rating > 5)
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 0 and 5.");

        _albumRatings.Add(rating);
    }
}