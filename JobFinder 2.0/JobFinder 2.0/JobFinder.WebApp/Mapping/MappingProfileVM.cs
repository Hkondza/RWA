using AutoMapper;
using BLL.DTOs.Firm;
using BLL.DTOs.JobApplication;
using BLL.DTOs.JobOffer;
using BLL.DTOs.Profile;
using BLL.DTOs.User;
using BLL.DTOs.UserFirm;
using BLL.DTOs.Worker;
using DAL.Models;
using JobFinder.WebApp.ViewModels.Admin;
using JobFinder.WebApp.ViewModels.Application;
using JobFinder.WebApp.ViewModels.Auth;
using JobFinder.WebApp.ViewModels.JobOffer;
using JobFinder.WebApp.ViewModels.Profile;
using JobFinder.WebApp.ViewModels.Worker;

namespace JobFinder.WebApp.Mapping
{
    public class MappingProfileVM : Profile
    {
        public MappingProfileVM()
        {

            CreateMap<JobOfferReadDto,JobOfferDetailsVM>()
                .ForMember(dest => dest.IDJobOffer, opt => opt.MapFrom(s => s.IDJobOffer))
                .ForMember(d => d.FirmName, opt => opt.MapFrom(s => s.FirmName))
                .ForMember(d => d.JobName, opt => opt.MapFrom(s => s.JobName))
                .ForMember(d => d.LocationName, opt => opt.MapFrom(s => s.LocationName));


            CreateMap<JobOfferReadDto, JobOfferDetailsVM>()
                 .ForMember(dest => dest.IDJobOffer, opt => opt.MapFrom(s => s.IDJobOffer))
                 .ForMember(d => d.FirmName, opt => opt.MapFrom(s => s.FirmName))
                 .ForMember(d => d.JobName, opt => opt.MapFrom(s => s.JobName))
                 .ForMember(d => d.LocationName, opt => opt.MapFrom(s => s.LocationName));


           // Skontaj sta dto za create vraca i pogledaj create za offer (pogledaj kontroler)

            //CreateMap<JobOfferCreateDto, JobOfferCreateVM>()
            //     .ForMember(d => d.FirmName, opt => opt.MapFrom(s => s.))
            //     .ForMember(d => d.JobName, opt => opt.MapFrom(s => s.JobName))
            //     .ForMember(d => d.LocationName, opt => opt.MapFrom(s => s.LocationName));


            CreateMap<ProfileReadDto, ProfileVM>();
            CreateMap<ProfileUpdateDto, ProfileUpdateVM>();
            CreateMap<ProfileUpdateVM, ProfileUpdateDto>();


            CreateMap<ChangePasswordDto, ChangePasswordVM>();

            CreateMap<JobOfferCreateDto, JobOfferCreateVM>();
            CreateMap<JobOfferCreateVM, JobOfferCreateDto>();

            CreateMap<JobOfferReadDto, JobOfferListVM>();
            CreateMap<JobOfferReadDto, JobOfferDetailsVM>();
            CreateMap<JobOfferReadDto, JobOfferDetailsVM>();

            CreateMap<JobApplicationVM, JobApplicationCreateDto>();
            CreateMap<JobApplicationReadDto, JobApplicationDetailsVM>();
            CreateMap<JobApplicationReadDto, WorkerDetailsVM>()
                .ForMember(d => d.JobApplicationId, opt => opt.MapFrom(s => s.IDJobApplication));
            CreateMap<JobApplicationDetailsVM, JobApplicationReadDto>();
         
            CreateMap<JobApplicationReadDto, JobApplicationListVM>();
            CreateMap<JobApplicationReadDto, JobApplicationUsers>();


            CreateMap<Log, LogVM>();



            CreateMap<UserFirm, UserFirmReadDto>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.User.Username))
                .ForMember(d => d.FirmName, o => o.MapFrom(s => s.Firm.FirmName));

            CreateMap<RegisterVM, UserRegisterDto>();
            CreateMap<UserRegisterDto,RegisterVM>();


            CreateMap<WorkerReadDto, WorkerDetailsVM>();


            CreateMap<ChangePasswordDto, ChangePasswordVM>();
            CreateMap<ChangePasswordVM, ChangePasswordDto>();
            


           
            CreateMap<User, UserReadDto>();



            CreateMap<UserFirm, UserFirmAdminVM>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.User.Username))
                .ForMember(d => d.FirmName, o => o.MapFrom(s => s.Firm.FirmName));




            CreateMap<LoginResponseDto, LoginVM>();
            CreateMap<LoginVM, LoginResponseDto>();
            CreateMap<LoginVM, UserLoginDto>();



            CreateMap<FirmReadDto, FirmLookupVM>();
            CreateMap<FirmRequestDto, FirmRequestVM>();
            CreateMap<FirmRequestVM, FirmRequestDto>();

            

            CreateMap<WorkerCreateDto, Worker>();
            CreateMap<Worker, WorkerReadDto>()
                .ForMember(d => d.IDWorker, opt => opt.MapFrom(s => s.Idworker))
                .ForMember(d => d.JobApplicationId, opt => opt.MapFrom(s => s.JobApplication.IdjobApplication))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.JobApplication.User.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.JobApplication.User.LastName));


        }
    }
}
