using AutoMapper;
using web_api_assignment.Models.DTOS;
using web_api_assignment.Models.Entities;

namespace web_api_assignment.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile() 
        {
            CreateMap<Character, CharacterDto>()
                .ForMember(dto => dto.Movies, opt => opt
                .MapFrom(p => p.Movies.Select(s => s.Id).ToList()));

        }
    }
}
