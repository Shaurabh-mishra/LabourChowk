using LabourChowk_webapi.Models;
using LabourChowk_webapi.Reporsitories.Interfaces;
using LabourChowk_webapi.Repositories;
using LabourChowk_webapi.Services.Interfaces;

namespace LabourChowk_webapi.Services
{
    public class JobService : IJobService
    {
        private readonly IGenericRepository<Job> _repository;

        public JobService(IGenericRepository<Job> repository)
        {
            _repository = repository;
        }

        // Get all jobs
        public Task<List<Job>> GetAllJobsAsync()
        {
            return _repository.GetAllAsync(includeProperties: "WorkPoster");
        }

        // Get a job by ID
        public Task<Job?> GetJobByIdAsync(int id) => _repository.GetByIdAsync(id);

        // Add new job
        public Task<Job> AddJobAsync(Job job) => _repository.AddAsync(job);

        // Update job
        public async Task<bool> UpdateJobAsync(int id, Job updatedJob)
        {
            var job = await _repository.GetByIdAsync(id);
            if (job == null) return false;

            // Business logic: update only allowed fields
            job.Title = updatedJob.Title;
            job.Description = updatedJob.Description;
            job.WorkPosterId = updatedJob.WorkPosterId;
            job.WorkPoster = updatedJob.WorkPoster;

            await _repository.UpdateAsync(job);
            return true;
        }

        // Delete job
        public async Task<bool> DeleteJobAsync(int id)
        {
            var job = await _repository.GetByIdAsync(id);
            if (job == null) return false;

            await _repository.DeleteAsync(job);
            return true;
        }
    }
}
