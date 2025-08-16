using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabourChowk_webapi.Models
{
    public class Job
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? WorkPosterId { get; set; }

        public WorkPoster? WorkPoster { get; set; } // navigation property
    }
}