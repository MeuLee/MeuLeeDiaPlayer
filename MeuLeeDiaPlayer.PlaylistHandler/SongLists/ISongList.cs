using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.PlaylistHandler.PlaylistPlayMode;
using System.Collections.Generic;

namespace MeuLeeDiaPlayer.PlaylistHandler.SongLists
{
    public interface ISongList
    {
        SongDto CurrentSong { get; }
        IReadOnlyList<SongDto> FollowingSongs { get; }
        PlaylistDto Playlist { get; set; }
        PlayMode PlayMode { get; set; }

        SongList MoveNext();
        SongList MovePrevious();
    }
}