using AutoMapper;
using LabourChowk_webapi.DTOs;
using LabourChowk_webapi.Models;
//using LabourChowk_webapi.Entities;

namespace LabourChowk_webapi.Mappings
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            // Map from DTO to Entity
            CreateMap<JobCreateDTO, Job>();

            // Map from Entity to DTO
            CreateMap<Job, JobResponseDTO>()
                .ForMember(dest => dest.PostedBy,
                        opt => opt.MapFrom(src => src.WorkPoster != null ? src.WorkPoster.Name : null));
            CreateMap<JobResponseDTO, Job>();
        }
    }
}