using AutoMapper;
using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.EntityFramework.Repository;
using MeuLeeDiaPlayer.Services.PlaylistHolders;
using Meziantou.Framework.WPF.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.Services.PlaylistRetrievers
{
    public class PlaylistRetriever : IPlaylistRetriever
    {
        private readonly IModelRepository<Playlist> _playlistRepository;
        private readonly IMapper _mapper;
        private readonly IPlaylistHolder _playlistHolder;

        public PlaylistRetriever(IModelRepository<Playlist> playlistRepository, IPlaylistHolder playlistHolder, IMapper mapper)
        {
            _playlistRepository = playlistRepository;
            _mapper = mapper;
            _playlistHolder = playlistHolder;
        }

        public async Task LoadPlaylists()
        {
            var playlists = (await _playlistRepository.GetAsync()).ToList();
            _playlistHolder.Playlists = new ConcurrentObservableCollection<PlaylistDto>();
            playlists.ForEach(p => _playlistHolder.Playlists.Add(_mapper.Map<PlaylistDto>(p)));
        }
    }
}
