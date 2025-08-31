using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabourChowk_webapi.Models;

namespace LabourChowk_webapi.Services.Interfaces
{
    public interface IJobService
    {
        public Task<List<Job>> GetAllJobsAsync();
        public Task<Job?> GetJobByIdAsync(int id);
        public Task<Job> AddJobAsync(Job job);
        public Task<bool> UpdateJobAsync(int id, Job updatedJob);
        public Task<bool> DeleteJobAsync(int id);
    }
}