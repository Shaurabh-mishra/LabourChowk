using Microsoft.AspNetCore.Mvc;
using LabourChowk_webapi.Models;
using LabourChowk_webapi.Services;
using AutoMapper;
using LabourChowk_webapi.DTOs;
using LabourChowk_webapi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LabourChowk_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "WorkPoster,admin")] // Only WorkPoster and Admin can manage jobs
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;

        public JobController(IJobService jobService, IMapper mapper)
        {
            _jobService = jobService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobResponseDTO>>> GetJobs()
        {
            // Use the service to get all jobs
            var jobs = await _jobService.GetAllJobsAsync();
            if (jobs == null || !jobs.Any()) return NotFound("No jobs found.");

            // Map the jobs to JobResponseDTO
            var jobDtos = _mapper.Map<IEnumerable<JobResponseDTO>>(jobs);

            // Return the mapped DTOs
            return Ok(jobDtos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<JobResponseDTO>> GetJob(int id)
        {
            var job = await _jobService.GetJobByIdAsync(id);
            if (job is null) return NotFound($"Job with ID {id} not found.");

            // Map the job to JobResponseDTO
            var jobDto = _mapper.Map<JobResponseDTO>(job);

            return Ok(jobDto);
        }

        [HttpPost]
        public async Task<ActionResult<JobCreateDTO>> AddJob([FromBody] JobCreateDTO jobDto)
        {
            if (jobDto == null) return BadRequest("Job data is null.");

            // Map the DTO to Job entity
            var job = _mapper.Map<Job>(jobDto);

            // Use the service to add the job
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (job.WorkPosterId <= 0) return BadRequest("Invalid WorkPosterId.");

            // Add the job and return the created response

            var newJob = await _jobService.AddJobAsync(job);
            return CreatedAtAction(nameof(GetJob), new { id = newJob.Id }, newJob);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] JobCreateDTO jobDto)
        {
            if (jobDto == null) return BadRequest("Job data is null.");

            // Map the DTO to Job entity
            var job = _mapper.Map<Job>(jobDto);
            job.Id = id; // Ensure the ID is set for the update

            // Validate the model state
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (job.WorkPosterId <= 0) return BadRequest("Invalid WorkPosterId.");

            // Use the service to update the job

            var success = await _jobService.UpdateJobAsync(id, job);
            if (!success) return NotFound();
            return Ok(job); // Return the updated job as the response
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteJob(int id)
        {
            var success = await _jobService.DeleteJobAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
