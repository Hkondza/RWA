using AutoMapper;
using JobFinder.WebAPI.DTOs.Firm;
using JobFinder.WebAPI.DTOs.JobApplication;
using JobFinder.WebAPI.DTOs.JobOffer;
using JobFinder.WebAPI.DTOs.User;
using JobFinder.WebAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobFinder.WebAPI.Mapping
{
    public class JobFinderProfile : Profile
    {
        public JobFinderProfile()
        {
            
            CreateMap<JobOffer, JobOfferReadDto>()
    .ForMember(dest => dest.IDJobOffer, opt => opt.MapFrom(s => s.IDJobOffer))
    .ForMember(d => d.FirmName, opt => opt.MapFrom(s => s.Firm.FirmName))
    .ForMember(d => d.JobName, opt => opt.MapFrom(s => s.JobType.JobName))
    .ForMember(d => d.LocationName, opt => opt.MapFrom(s => s.Location.LocationName));

            CreateMap<JobOfferCreateDto, JobOffer>();
            CreateMap<JobApplicationCreateDto, JobApplication>();
            CreateMap<JobApplication, JobApplicationReadDto>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<User, UserReadDto>();
            CreateMap<Firm,FirmReadDto>();
            CreateMap<FirmCreateDto, Firm>();
        }
    }
}
