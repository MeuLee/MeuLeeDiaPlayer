using AutoMapper;
using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.EntityFramework.DbModels;

namespace MeuLeeDiaPlayer.Common.Profiles
{
    public class SongProfile : Profile
    {
        public SongProfile()
        {
            CreateMap<Song, SongDto>()
                .ForMember(dto => dto.FileReader, opt => opt.Ignore());
            CreateMap<SongDto, Song>();
        }
    }
}
