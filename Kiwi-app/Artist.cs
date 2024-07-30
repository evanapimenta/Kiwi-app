﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwi_app
{
    /// <summary>
    /// Represents an artist/a band in the system, that may have albums and tracks associated with it.
    /// </summary>
    /// <param name="artistName">The name of the band/artist.</param>
    internal class Artist(string Name)
    {
        private static readonly HashSet<int> artistIds = new();
        private static readonly Random random = new();

        public string Name { get; } = Name ?? throw new ArgumentNullException(nameof(Name), "Name cannot be null");
        public int Id { get; private set; } = GenerateUniqueArtistId();
        public List<Album> Albums { get; } = new();
        public List<Track> Tracks { get; } = new();

        /// <summary>
        /// Generates an unique user ID to be added to artistIds.
        /// </summary>
        /// <returns>A unique integer value.</returns>
        public static int GenerateUniqueArtistId()
        {
            int newArtistId;

            do
            {
                newArtistId = random.Next(1, 1000000);

            } while (!Artist.AddArtistId(newArtistId));

            return newArtistId;
        }


        /// <summary>
        /// Add the unique ID generated by <see cref="GenerateUniqueArtistId"/> to the artistIds HashSet.
        /// </summary>
        /// <param name="newArtistId"></param>
        /// <returns>
        /// <see langword="true"/> if the artist ID was successfully added to the HashSet (i.e., it was unique and not previously present); 
        /// <see langword="false"/> if the artist ID was already in the HashSet (i.e., it was not unique).
        ///</returns>
        public static bool AddArtistId(int newArtistId)
        {
            return artistIds.Add(newArtistId);
        }


        /// <summary>
        /// Adds an album to the artist.
        /// </summary>
        /// <param name="album">The album to be added.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="album">parameter is null.</exception>
        public void AddAlbumToArtist(Album album)
        {
            if (album == null)
            {
                throw new ArgumentNullException(nameof(album), "album cannot be null");
            }

            if (!Albums.Contains(album))
            {
                Albums.Add(album);

            }

        }

        /// <summary>
        /// Adds track to artist.
        /// </summary>
        /// <param name="track">The track to be added to the artist's catalog.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="track"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the track's artist does not match this artist./></exception>
        public void AddTrackToArtist(Track track)
        {
            if (track == null) throw new ArgumentNullException(nameof(track), "Track cannot be null");
            if (track.Artist != this)
            {
                throw new InvalidOperationException("Track's artist does not match this artist.");
            }
            if (!Tracks.Contains(track))
            {
                Tracks.Add(track);
            }
        }

    }
}
