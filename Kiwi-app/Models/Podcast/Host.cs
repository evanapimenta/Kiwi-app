using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Kiwi_app.Models.Music;

namespace Kiwi_app.Models.Podcast.Podcast;

/// <summary>
/// Represents a Podcast host in the system.
/// </summary>
/// <param name="HostName">The name of the Podcast's host.</param>
internal class Host(string _hostName)
{
    private static HashSet<int> hostIds { get; } = new();
    private static readonly Random random = new();
    private static readonly object lockObject = new();

    public string Name
    {
        get => _hostName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value), "Name cannot be null or empty");
            }

            _hostName = value;
        }
    }
    public int HostId { get; } = GenerateUniqueHostId();

    /// <summary>
    /// Generates an unique ID to the host.
    /// </summary>
    /// <returns>A unique integer value that will represent a host instance.</returns>
    public static int GenerateUniqueHostId()
    {
        int newHostId;
        Random random = new();

        lock (lockObject)
        {
            do
            {
                newHostId = random.Next(0, 500);

            } while (!Genre.AddGenreId(newHostId));
        }

        return newHostId;
    }

    /// <summary>
    /// Adds the unique ID to the hostIds HashSet, ensuring that there are no duplicates.
    /// </summary>
    /// <param name="hostId">An unique ID that will be linked to a host in the system.</param>
    /// <returns>
    /// <see langword="true"/> if the host ID was successfully added to the HashSet (i.e., it was unique and not previously present); 
    /// <see langword="false"/> if the host ID was already in the HashSet (i.e., it was not unique).
    /// </returns>

    public static bool AddGenreId(int hostId)
    {
        return hostIds.Add(hostId);
    }
}
