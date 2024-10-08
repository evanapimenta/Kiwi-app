﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwi_app.Models.Podcast;

/// <summary>
/// Represents a Podcast in the system.
/// </summary>
/// <param name="Host">Represents a Host to which the Podcast will be linked to</param>
/// <param name="PodcastName">The name of the Podcast.</param>
internal class Podcast(Host Host, string _podcastName)
{
    private static readonly HashSet<int> podcastIds = new();
    private static readonly Random random = new();
    private static readonly object lockObject = new();

    public Host Host { get; } = Host ?? throw new ArgumentNullException(nameof(Host), "Host name cannot be null");
    public int PodcastId { get; } = GenerateUniquePodcastId();

    public string PodcastName
    {
        get => _podcastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value), "Podcast name cannot be null or empty");
            }

            _podcastName = value;
        }
    }

    private List<Episode> _episodes = new();
    public IReadOnlyList<Episode> Episodes => _episodes.AsReadOnly();

    public int EpisodeNumber => Episodes.Count;

    private List<int> _podcastRatings = new();
    public IReadOnlyList<int> PodcastRatings => _podcastRatings.AsReadOnly();

    public double PodcastAverageRating => PodcastRatings.Count == 0 ? 0 : PodcastRatings.Average();

    public string PodcastInfo => $"Podcast Info:\n" +
           $"Title: {PodcastName}" +
           $"Host: {Host}" +
           $"List of episodes:\n" +
           $"{string.Join("\n", Episodes.OrderBy(e => e.EpisodeNumber).Select(e => e.ToString()))}";

    /// <summary>
    /// Generates a unique ID for every podcast.
    /// </summary>
    /// <returns>A unique integer ID for an instance of a Podcast.</returns>
    public static int GenerateUniquePodcastId()
    {
        int newPodcastId;

        lock (lockObject)
        {
            do
            {
                newPodcastId = random.Next(1, 1000000);
            } while (!AddPodcastId(newPodcastId));
        }

        return newPodcastId;
    }

    /// <summary>
    /// Add the unique ID generated by <see cref="GenerateUniquePodcastId"/> to the podcastIds HashSet.
    /// </summary>
    /// <param name="newPodcastId">An unique integer to be added to the podcastIds HashSet, thus being associated with a podcast.</param>
    /// <returns>
    /// <see langword="true"/> if the podcast ID was successfully added to the HashSet (i.e., it was unique and not previously present); 
    /// <see langword="false"/> if the podcast ID was already in the HashSet (i.e., it was not unique).
    ///</returns>
    public static bool AddPodcastId(int newPodcastId)
    {
        return podcastIds.Add(newPodcastId);
    }

    /// <summary>
    /// Adds an episode to a Podcast.
    /// </summary>
    /// <param name="episode">The episode to be added to the podcast.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="episode"/>is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the <paramref name="episode"/> linked to a podcast does not match the podcast the user is currently trying to link it to.</exception>
    public void AddEpisode(Episode episode)
    {
        if (episode == null)
            throw new ArgumentNullException(nameof(episode), "Episode cannot be null");
        if (episode.Podcast != this)
            throw new InvalidOperationException("Episode podcast doesn't match");

        _episodes.Add(episode);
    }
}
