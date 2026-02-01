using AutoMapper;
using BLL.DTOs.Firm;
using BLL.DTOs.JobApplication;
using BLL.DTOs.JobOffer;
using BLL.DTOs.User;
using BLL.DTOs.UserFirm;
using BLL.DTOs.Worker;
using DAL.Models;

namespace BLL.Mapping
{
    public class JobFinderProfile : Profile
    {
        public JobFinderProfile()
        {
            
            CreateMap<JobOffer, JobOfferReadDto>()
                .ForMember(dest => dest.IDJobOffer, opt => opt.MapFrom(s => s.IdjobOffer))
                .ForMember(d => d.FirmName, opt => opt.MapFrom(s => s.Firm.FirmName))
                .ForMember(d => d.JobName, opt => opt.MapFrom(s => s.JobType.JobName))
                .ForMember(d => d.LocationName, opt => opt.MapFrom(s => s.Location.LocationName));

            CreateMap<JobOfferCreateDto, JobOffer>();
            CreateMap<JobApplicationCreateDto, JobApplication>();

            CreateMap<JobApplication, JobApplicationReadDto>()
                .ForMember(dest => dest.IDJobApplication, opt => opt.MapFrom(s => s.IdjobApplication))
                .ForMember(d => d.JobOfferID, opt => opt.MapFrom(s => s.JobOffer.IdjobOffer))
                .ForMember(d => d.FirmName, opt => opt.MapFrom(s => s.JobOffer.Firm.FirmName))
                .ForMember(d => d.JobName, opt => opt.MapFrom(s => s.JobOffer.JobType.JobName))
                .ForMember(d => d.LocationName, opt => opt.MapFrom(s => s.JobOffer.Location.LocationName))
                .ForMember(d => d.Salary, opt => opt.MapFrom(s => s.JobOffer.Salary))
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.JobOffer.Title))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.JobOffer.Description))
                .ForMember(d => d.UserID, opt => opt.MapFrom(s => s.UserId))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.User.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.User.LastName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email))
                .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.User.Phone))
                .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.JobOffer.IsActive));

            CreateMap<UserFirm, UserFirmReadDto>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.User.Username))
                .ForMember(d => d.FirmName, o => o.MapFrom(s => s.Firm.FirmName));

            CreateMap<UserRegisterDto, User>();
            CreateMap<User, UserReadDto>();
            CreateMap<Firm,FirmReadDto>();
            CreateMap<Firm,FirmUpdateDto>()
                .ForMember(d => d.IDFirm, o => o.MapFrom(s => s.Idfirm));
            CreateMap<FirmUpdateDto, Firm>()
               .ForMember(d => d.Idfirm, o => o.MapFrom(s => s.IDFirm));

            CreateMap<FirmCreateDto, Firm>();

            CreateMap<WorkerCreateDto, Worker>();
            CreateMap<Worker, WorkerReadDto>()
                .ForMember(d => d.IDWorker, opt => opt.MapFrom(s => s.Idworker))
                .ForMember(d => d.JobApplicationId, opt => opt.MapFrom(s => s.JobApplication.IdjobApplication))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.JobApplication.User.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.JobApplication.User.LastName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.JobApplication.User.Email));
                

        }
    }
}
