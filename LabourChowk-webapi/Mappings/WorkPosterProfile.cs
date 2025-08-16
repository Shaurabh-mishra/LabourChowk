using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LabourChowk_webapi.DTOs;
using LabourChowk_webapi.Models;

namespace LabourChowk_webapi.Mappings
{
    public class WorkPosterProfile : Profile
    {
        public WorkPosterProfile()
        {
            CreateMap<WorkPoster, WorkPosterResponseDto>();
            CreateMap<WorkPosterCreateDto, WorkPoster>();
            CreateMap<WorkPosterUpdateDto, WorkPoster>();
        }
    }
}