using AutoMapper;
using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.EntityFramework.Repository;
using MeuLeeDiaPlayer.Services.PlaylistHolders;
using Meziantou.Framework.WPF.Collections;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.Services.PlaylistRetrievers
{
    public class PlaylistRetriever : IPlaylistRetriever
    {
        private readonly IRepository<Playlist> _playlistRepository;
        private readonly IMapper _mapper;
        private readonly IPlaylistHolder _playlistHolder;

        public PlaylistRetriever(IRepository<Playlist> playlistRepository, IPlaylistHolder playlistHolder, IMapper mapper)
        {
            _playlistRepository = playlistRepository;
            _mapper = mapper;
            _playlistHolder = playlistHolder;
        }

        public async Task LoadPlaylists()
        {
            var playlists = await _playlistRepository.GetAllAsync(p => p.Songs);
            _playlistHolder.Playlists = new ConcurrentObservableCollection<PlaylistDto>();
            playlists.ForEach(p => _playlistHolder.Playlists.Add(_mapper.Map<PlaylistDto>(p)));
        }
    }
}
