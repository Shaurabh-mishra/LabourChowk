using AutoMapper;
using LabourChowk_webapi.DTOs;
using LabourChowk_webapi.Models;
using LabourChowk_webapi.Services;
using LabourChowk_webapi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabourChowk_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        //inject dependencies
        private readonly IWorkerService _workerService;
        private readonly IMapper _mapper;

        public WorkerController(IWorkerService workerService, IMapper mapper)
        {
            _workerService = workerService;
            _mapper = mapper;
        }

        // Controller actions go here
        [HttpGet]
        [Authorize(Roles = "WorkPoster,Admin")] // Only Workers and Admin can access this endpoint
        public async Task<ActionResult<IEnumerable<WorkerResponseDto>>> GetWorkersAsync()
        {
            // Logic to retrieve workers
            var workers = await _workerService.GetAllWorkersAsync();
            if (workers == null || !workers.Any())
            {
                return NotFound(); // returns 404 Not Found if no workers are found
            }
            var workerDtos = _mapper.Map<IEnumerable<WorkerResponseDto>>(workers);
            return Ok(workerDtos); // returns 200 OK with data
        }
        [HttpGet("{id}", Name = "GetWorkerById")]
        public async Task<ActionResult<WorkerResponseDto>> GetWorkerByIdAsync(int id)
        {
            var worker = await _workerService.GetWorkerByIdAsync(id);
            if (worker is null)
            {
                return NotFound(); // returns 404 Not Found if worker not found
            }
            var workerDto = _mapper.Map<WorkerResponseDto>(worker);
            return Ok(workerDto); // returns 200 OK with data
        }
        [HttpPost]
        public async Task<ActionResult<WorkerResponseDto>> AddWorkerAsync([FromBody] WorkerCreateDto workerDto)
        {
            if (workerDto == null)
            {
                return BadRequest("Worker data is null."); // returns 400 Bad Request if data is null
            }

            var worker = _mapper.Map<Worker>(workerDto);
            var addedWorker = await _workerService.AddWorkerAsync(worker);
            var addedWorkerDto = _mapper.Map<WorkerResponseDto>(addedWorker);

            return CreatedAtAction("GetWorkerById", new
            {
                id = addedWorker.Id
            }, addedWorkerDto); // returns 201 Created with location
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<WorkerResponseDto>> UpdateWorkerAsync(int id, [FromBody] WorkerResponseDto workerDto)
        {
            if (workerDto == null)
            {
                return BadRequest("Worker data is null."); // returns 400 Bad Request if data is null
            }

            var existingWorker = await _workerService.GetWorkerByIdAsync(id);
            if (existingWorker == null)
            {
                return NotFound(); // returns 404 Not Found if worker not found
            }

            var worker = _mapper.Map<Worker>(workerDto);
            worker.Id = id; // ensure the ID is set

            var updatedWorker = await _workerService.UpdateWorkerAsync(id, worker);
            // var updatedWorkerDto = _mapper.Map<WorkerResponseDto>(updatedWorker);

            return Ok(updatedWorker); // returns 200 OK with data
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWorkerAsync(int id)
        {
            var existingWorker = await _workerService.GetWorkerByIdAsync(id);
            if (existingWorker == null)
            {
                return NotFound(); // returns 404 Not Found if worker not found
            }

            await _workerService.DeleteWorkerAsync(id);
            return NoContent(); // returns 204 No Content on successful deletion
        }

        [HttpPost("CreateValidated")]
        public async Task<ActionResult<WorkerResponseDto>> AddWorkerWithValidationAsync([FromBody] WorkerCreateDto workerDto)
        {
            if (workerDto == null)
            {
                return BadRequest("Worker data is null."); // returns 400 Bad Request if data is null
            }

            var worker = _mapper.Map<Worker>(workerDto);
            (Worker? addedWorker, string? errorMessage) = await _workerService.AddWorkerWithValidationAsync(worker);

            if (addedWorker == null)
            {
                return BadRequest(errorMessage); // returns 400 Bad Request with error message
            }

            var addedWorkerDto = _mapper.Map<WorkerResponseDto>(addedWorker);
            return CreatedAtAction("GetWorkerById", new { id = addedWorker.Id }, addedWorkerDto); // returns 201 Created with location
        }
    }
}
