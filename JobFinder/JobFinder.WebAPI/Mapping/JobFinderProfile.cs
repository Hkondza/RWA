using AutoMapper;
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
            CreateMap<JobOffer, JobOfferReadDto>();
            CreateMap<JobOfferCreateDto, JobOffer>();
            CreateMap<JobApplicationCreateDto, JobApplication>();
            CreateMap<JobApplication, JobApplicationReadDto>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<User, UserReadDto>();
        }
    }
}
