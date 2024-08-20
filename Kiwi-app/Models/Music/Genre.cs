using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwi_app.Models.Music;
/// <summary>
/// Represents a music genre in the system that may be linked to a track, album or artist.
/// </summary>
/// <param name="Name">The name of the musical genre</param>

internal class Genre(string _name)
{
    private static readonly HashSet<int> genreIds = new();
    private static readonly object lockObject = new();

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value), "Name cannot be null or empty");
            }

            _name = value;
        }
    }

    public int GenreId { get; } = GenerateUniqueGenreId();

    /// <summary>
    /// Generates an unique ID to the genre.
    /// </summary>
    /// <returns>A unique integer value that will represent a certain genre.</returns>
    public static int GenerateUniqueGenreId()
    {
        int newGenreId;
        Random random = new();

        lock (lockObject)
        {
            do
            {
                newGenreId = random.Next(0, 500);

            } while (!AddGenreId(newGenreId));
        }

        return newGenreId;
    }

    /// <summary>
    /// Adds the unique ID to the genreIds HashSet, ensuring that there are no duplicates.
    /// </summary>
    /// <param name="genreId">An unique ID that will be linked to a genre in the system.</param>
    /// <returns>
    /// <see langword="true"/> if the genre ID was successfully added to the HashSet (i.e., it was unique and not previously present); 
    /// <see langword="false"/> if the genre ID was already in the HashSet (i.e., it was not unique).
    /// </returns>

    public static bool AddGenreId(int genreId)
    {
        return genreIds.Add(genreId);
    }
}
