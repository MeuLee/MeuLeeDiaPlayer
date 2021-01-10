using AutoMapper;
using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using System.Linq;

namespace MeuLeeDiaPlayer.Common.Profiles
{
    public class PlaylistProfile : Profile
    {
        public PlaylistProfile()
        {
            CreateMap<Playlist, PlaylistDto>()
                .ForMember(dto => dto.Songs, opt => opt.MapFrom(p => p.PlaylistSongs.Select(ps => ps.Song)));
        }
    }
}
