using AutoMapper;
using web_api_assignment.Models.DTOS.Characters;
using web_api_assignment.Models.Entities;

namespace web_api_assignment.Profiles
{
    public class CharacterProfile : Profile
    {
        /// <summary>
        /// Profile for mapping entities to dtos
        /// Used for mapping
        /// </summary>
        public CharacterProfile() 
        {
            CreateMap<CharacterPostDto, Character>();
            CreateMap<CharacterPutDto, Character>();
            CreateMap<Character, CharacterDto>()
                .ForMember(dto => dto.Movies, opt => opt
                .MapFrom(p => p.Movies.Select(s => s.Id).ToList()));
        }
    }
}
