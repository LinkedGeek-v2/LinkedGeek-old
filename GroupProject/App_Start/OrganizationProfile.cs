using AutoMapper;
using GroupProject.ApiModels.AssosiativeDTOs;
using GroupProject.ApiModels.ChatDTOs;
using GroupProject.ApiModels.DeveloperDtos;
using GroupProject.ApiModels.DeveloperDTOs;
using GroupProject.ApiModels.FixedDTOs;
using GroupProject.ApiModels.Incoming.ProfilePage;
using GroupProject.ApiModels.PostsDTOs;
using GroupProject.ApiModels.SharedDTOs;
using GroupProject.ApiModels.UserDTOs;
using GroupProject.Models;
using GroupProject.Models.AssociativeModels;
using GroupProject.Models.CompanyModels;
using GroupProject.Models.DeveloperModels;
using GroupProject.Models.FixedModels;
using GroupProject.Models.SharedModels;
using GroupProject.ViewModels;
using GroupProject.ViewModels.CompanyViewModels;
using GroupProject.ViewModels.DeveloperViewModels.ProfilePageViewModels;

namespace GroupProject.App_Start
{

    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<DeveloperFormViewModel, Developer>();
            CreateMap<CompanyFormViewModel, Company>();
            CreateMap<Company, CompanyFormViewModel>();

            CreateMap<Job, JobGetDto>();

            CreateMap<Developer, DeveloperDto>();

            CreateMap<ApplicationUser, ApplicationUserDto>();

            CreateMap<Address, AddressDto>();
            //CreateMap<AddressDto, Address>();   Check if Needed

            CreateMap<City, CityDto>();

            CreateMap<Country, CountryDto>();

            CreateMap<Education, EducationDto>();

            CreateMap<Experience, ExperienceDto>();

            CreateMap<Skill, SkillDto>();

            CreateMap<DeveloperSkills, DeveloperSkillsDto>();

            CreateMap<ApplicationUser, ApplicationUserNetworkDto>();
            CreateMap<Developer, DeveloperNetworkDto>();
            CreateMap<Company, CompanyNetworkDto>();

            CreateMap<ApplicationUser, ChatApplicationUserDto>();
            CreateMap<Developer, ChatDeveloperDto>();
            CreateMap<Company, ChatCompanyDto>();

            //Viewmodels Mapping for profile page

            CreateMap<Developer, DeveloperProfilePageViewModel>();
            CreateMap<ApplicationUser, UserProfilePageViewModel>();
            CreateMap<Address, AddressProfilePageViewModel>();
            CreateMap<City, CityProfilePageViewModel>();
            CreateMap<Country, CountryProfilePageViewModel>();
            CreateMap<Education, EducationProfilePageViewModel>();
            CreateMap<Experience, ExperienceProfilePageViewModel>();
            CreateMap<Skill, SkillProfilePageViewModel>();
            CreateMap<DeveloperSkills, DeveloperSkillsProfilePageViewModel>();
            CreateMap<Company, CompanyNamesForDeveloperProfileViewModel>();

            //Incoming Dtos

            CreateMap<AddressPostDto, Address>();
            CreateMap<EducationPostDto, Education>();
            CreateMap<ExperiencePostDto, Experience>();

            // Post Dto
            CreateMap<Post, OutgoingPostDto>();
            CreateMap<OutgoingPostDto, Post>();
        }
    }
}
