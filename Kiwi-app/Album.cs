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
        public TimeSpan AlbumDuration  => AlbumTracks.Sum(track => TrackDuration.Ticks); 
        public List<Genre> TrackGenres { get; }




        public Album(string title, Artist artist, int releaseYear, List<Track> AlbumTracks)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Artist = artist ?? throw new ArgumentNullException(nameof(artist));
            if (releaseYear > 0) ReleaseYear = releaseYear; else throw new ArgumentNullException(nameof(releaseYear));
        }
    }
}
