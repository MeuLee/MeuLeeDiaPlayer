using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.PlaylistHandler.PlayModes;

namespace MeuLeeDiaPlayer.PlaylistHandler.SongLists
{
    public interface ISongList
    {
        SongDto CurrentSong { get; }
        PlaylistDto Playlist { get; set; }
        PlayMode PlayMode { get; set; }
        bool IsFirstSong { get; }

        void Play(SongDto song);
        SongList MoveNext();
        SongList MovePrevious();
    }
}