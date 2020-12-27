using AutoMapper;
using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.EntityFramework.DbModels;

namespace MeuLeeDiaPlayer.Common.Profiles
{
    public class PlaylistProfile : Profile
    {
        public PlaylistProfile()
        {
            CreateMap<Playlist, PlaylistDto>();
            CreateMap<PlaylistDto, Playlist>();
        }
    }
}
