using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabourChowk_webapi.DTOs
{
    public class WorkerCreateDto
    {
        public string Name { get; set; }
        public string Skill { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 numeric digits (0-9).")]
        public string Phone { get; set; }
    }
}