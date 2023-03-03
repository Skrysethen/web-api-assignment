using AutoMapper;
using web_api_assignment.Models.DTOS.Franchises;
using web_api_assignment.Models.Entities;

namespace web_api_assignment.Profiles
{
    public class FranchiseProfile : Profile
    {
        /// <summary>
        /// Profile for mapping entities to dtos
        /// Used for mapping
        /// </summary>
        public FranchiseProfile()
        {
            CreateMap<FranchisePostDto, Franchise>();
            CreateMap<FranchisePutDto, Franchise>();

            CreateMap<Franchise, FranchiseDto>()
                    .ForMember(dto => dto.Movies, opt => opt
                    .MapFrom(f => f.Movies.Select(m => m.Id).ToList()));

        }
    }
}