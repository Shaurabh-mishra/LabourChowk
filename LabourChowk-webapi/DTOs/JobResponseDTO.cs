using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabourChowk_webapi.DTOs
{
    public class JobResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Pay { get; set; }
        public string? PostedBy { get; set; }
    }
}