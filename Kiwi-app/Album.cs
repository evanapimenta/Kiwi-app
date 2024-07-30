using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwi_app
{
    /// <summary>
    /// Represents an album in the system that may have tracks associated with it.
    /// </summary>
    /// <param name="artist">The artist linked to an album.</param>
    /// <param name="albumTitle">The album title.</param>
    /// <param name="tracklist">A list of tracks that the album contains.</param>
    /// <param name="releaseYear">An integer number representing the year of release.</param>
    internal class Album(Artist Artist, string Title, List<Track> Tracklist, int ReleaseYear)
    {
        public Artist Artist { get; } = Artist ?? throw new ArgumentNullException(nameof(Artist), "Artist cannot be null");
        public string Title { get; } = Title ?? throw new ArgumentNullException(nameof(Title), "Title cannot be null");
        public List<Track> Tracklist { get; } = Tracklist ?? throw new ArgumentNullException(nameof(Tracklist), "Tracklist cannot be null");
        public TimeSpan AlbumDuration => TimeSpan.FromSeconds(Tracklist.Sum(track => track.Duration.TotalSeconds));
        public int ReleaseYear { get; } = ReleaseYear;

        /// <summary>
        /// Adds a song to an album.
        /// </summary>
        /// <param name="track">The track to be added to the album.</param>
        /// <exception cref="ArgumentNullException">Thrown when a <paramref name="track"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the <paramref name="track"/> does not belong to this album (if such a check is implemented).</exception>
        public void AddTrackToAlbum(Track track)
        {
            if (track == null)
            {
                throw new ArgumentNullException(nameof(track), "Track cannot be null");
            }

            if (track.Album != this)
            {
                throw new InvalidOperationException("Track's album does not match this album.");
            }

            if (!Tracklist.Contains(track)) Tracklist.Add(track);
        }

        /// <summary>
        /// Prints detailed information about the specified album, including its properties and tracklist.
        /// </summary>
        /// <param name="album">The album whose information is to be printed.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="album"/> is null.</exception>
        public static void PrintAlbumInfo(Album Album)
        {
            if (Album == null)
            {
                throw new ArgumentNullException(nameof(Album), "Album cannot be null");
            }

            Console.WriteLine("Album Info");
            var properties = Album.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(Album, null);
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
                else
                {
                    Console.WriteLine($"{property.Name}: {value}");
                }
            }
        }

    }
}