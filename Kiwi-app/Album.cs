using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwi_app
{
    internal class Album
    {
        public string Title { get; }
        public Artist Artist { get; }
        public int ReleaseYear { get; }
        public List<Track> AlbumTracks { get; }
        public TimeSpan AlbumDuration => TimeSpan.FromSeconds(AlbumTracks.Sum(track => track.TrackDuration.TotalSeconds));
        public List<Genre> TrackGenres { get; }


        public Album(string title, Artist artist, int releaseYear, List<Track> albumTracks, List<Genre> trackGenres)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Artist = artist ?? throw new ArgumentNullException(nameof(artist));
            if (releaseYear > 0) ReleaseYear = releaseYear; else throw new ArgumentNullException(nameof(releaseYear));
            AlbumTracks = AlbumTracks ?? throw new ArgumentNullException(nameof(albumTracks));
            TrackGenres = TrackGenres ?? throw new ArgumentNullException(nameof(trackGenres));
        }
    }
}
