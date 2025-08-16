using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabourChowk_webapi.DTOs
{
    public class WorkPosterCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Skill { get; set; }
        public string Phone { get; set; }
    }
}