using Kiwi_app.Models.Music;

Dictionary<string, Artist> artists = new();

//// Create Artist
Artist newArtist = new("The Cure");

//// Create Album
Album newAlbum = new(newArtist, "4:13 Dream", 2008);

//// Add Genres to Album


//// Create Tracks
Track track1 = new("Underneath the Stars", new TimeSpan(0, 6, 17));
Track track2 = new("The Only One", new TimeSpan(0, 3, 57));
// Add Tracks to Album
newAlbum.AddTrackToAlbum(track1);
newAlbum.AddTrackToAlbum(track2);

//// Add Album to Artist's discography
newArtist.AddAlbumToArtist(newAlbum);

newAlbum.PrintAlbumInfo();
