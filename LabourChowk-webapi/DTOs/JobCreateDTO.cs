using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabourChowk_webapi.Models;

namespace LabourChowk_webapi.DTOs
{
    public class JobCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Pay { get; set; }
        public int? WorkPosterId { get; set; }
        public WorkPoster? WorkPoster { get; set; }
    }
}