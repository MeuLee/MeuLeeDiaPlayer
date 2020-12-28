using AutoMapper;
using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.EntityFramework.Repository;
using MeuLeeDiaPlayer.Services.PlaylistHolders;
using System.Linq;
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
            _playlistHolder.Playlists = playlists.Select(p => _mapper.Map<PlaylistDto>(p)).ToList();
        }
    }
}
