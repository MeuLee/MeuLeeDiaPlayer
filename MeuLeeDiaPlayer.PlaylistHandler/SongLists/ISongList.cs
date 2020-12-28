using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.PlaylistHandler.PlayModes;
using System.Collections.Generic;

namespace MeuLeeDiaPlayer.PlaylistHandler.SongLists
{
    public interface ISongList
    {
        SongDto CurrentSong { get; }
        IReadOnlyList<SongDto> FollowingSongs { get; }
        PlaylistDto Playlist { get; set; }
        PlayMode PlayMode { get; set; }

        void Play(SongDto song);
        SongList MoveNext();
        SongList MovePrevious();
    }
}