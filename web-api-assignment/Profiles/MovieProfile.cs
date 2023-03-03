using AutoMapper;
using web_api_assignment.Models.DTOS.Movies;
using web_api_assignment.Models.Entities;

namespace web_api_assignment.Profiles
{
    public class MovieProfile: Profile
    {
        /// <summary>
        /// Profile for mapping entities to dtos
        /// Used for mapping
        /// </summary>
        public MovieProfile()
        {
            CreateMap<MoviePostDto, Movie>();
            CreateMap<MoviePutDto, Movie>();

            CreateMap<Movie, MovieDto>()
                    .ForMember(dto => dto.Characters, opt => opt
                    .MapFrom(m => m.Characters.Select(c => c.Id).ToList()));

        }
    }
}
