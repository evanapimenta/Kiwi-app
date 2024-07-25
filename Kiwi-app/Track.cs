using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwi_app
{
    internal class Track
    {
        public string Title { get; }
        public Artist Artist { get; }
        public Album Album { get; }
        public TimeSpan TrackDuration { get; }
        public List<Genre> TrackGenres { get; }

        public string SongInfo =>
            $"Song Info:\n" +
            $"Title: {Title}\n" +
            $"Artist: {Artist.Name}\n" +
            $"Album: {Album.Title}\n" +
            $"Genre: {string.Join(", ", TrackGenres)}\n)" +
            $"Length: {TrackDuration}";

        public Track(string title, Artist artist, Album album, TimeSpan trackDuration)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Artist = artist ?? throw new ArgumentNullException(nameof(artist));
            Album = album ?? throw new ArgumentNullException(nameof(album));
            TrackGenres = TrackGenres ?? throw new ArgumentNullException(nameof(TrackGenres));
           
            if (trackDuration != TimeSpan.Zero) TrackDuration = trackDuration; else throw new ArgumentNullException(nameof(TrackGenres));
        }
    }
}
