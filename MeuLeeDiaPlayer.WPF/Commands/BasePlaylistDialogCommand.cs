using AutoMapper;
using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.Services.SongLoaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.WPF.Commands
{
    public abstract class BasePlaylistDialogCommand : BaseCommand
    {
        private readonly IMapper _mapper;
        protected readonly ISongLoader _songLoader;

        public BasePlaylistDialogCommand(IMapper mapper, ISongLoader songLoader)
        {
            _mapper = mapper;
            _songLoader = songLoader;
        }

        public async Task<PlaylistDto> ModifyPlaylistInDb(
            Playlist playlist,
            IEnumerable<int> checkedSongsId,
            Func<Playlist, Task<Playlist>> crudOperation)
        {
            SetNewSongs(playlist, checkedSongsId);

            try
            {
                playlist = await crudOperation(playlist);
            }
            catch (Exception ex)
            {
                return null;
            }

            var playlistDto = _mapper.Map<PlaylistDto>(playlist);
            _songLoader.MapSongs(playlistDto);
            return playlistDto;
        }

        private void SetNewSongs(Playlist playlist, IEnumerable<int> songIds)
        {
            var dbSongs = songIds.Select(id => _songLoader.DbSongs.FirstOrDefault(s => s.Id == id));
            playlist.PlaylistSongs = dbSongs.Select(s => new PlaylistSong { Playlist = playlist, Song = s }).ToList();
        }
    }
}
