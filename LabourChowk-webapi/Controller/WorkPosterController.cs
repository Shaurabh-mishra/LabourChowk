using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LabourChowk_webapi.DTOs;
using LabourChowk_webapi.Models;
using LabourChowk_webapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabourChowk_webapi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkPosterController : ControllerBase
    {
        private readonly WorkPosterService _workePosterService;
        private readonly IMapper _mapper;

        public WorkPosterController(WorkPosterService workePosterService, IMapper mapper)
        {
            _workePosterService = workePosterService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkPosterResponseDto>>> GetWorkPostersAsync()
        {
            var workPosters = await _workePosterService.GetAllWorkPostersAsync();
            if (workPosters == null || !workPosters.Any())
            {
                return NotFound();
            }
            var workPostersDtos = _mapper.Map<IEnumerable<WorkPosterResponseDto>>(workPosters);
            return Ok(workPostersDtos);
        }
        [HttpGet("{id}",Name="GetWorkPosterById")]
        public async Task<ActionResult<WorkPosterResponseDto>> GetWorkPosterByIdAsync(int id)
        {
            var workPoster = await _workePosterService.GetWorkPosterByIdAsync(id);
            if (workPoster == null)
            {
                return NotFound();
            }
            var workPosterDto = _mapper.Map<WorkPosterResponseDto>(workPoster);
            return Ok(workPosterDto);
        }
        [HttpPost]
        public async Task<ActionResult<WorkPosterResponseDto>> CreateWorkPosterAsync(WorkPosterCreateDto workPosterRequestDto)
        {
            var workPoster = _mapper.Map<WorkPoster>(workPosterRequestDto);
            await _workePosterService.CreateWorkPosterAsync(workPoster);
            var createdWorkPosterDto = _mapper.Map<WorkPosterResponseDto>(workPoster);
            return CreatedAtAction("GetWorkPosterById", new { id = createdWorkPosterDto.Id }, createdWorkPosterDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateWorkPosterAsync(int id, WorkPosterUpdateDto workPosterUpdateDto)
        {
            var existingPoster = await _workePosterService.GetWorkPosterByIdAsync(id);
            if (existingPoster == null)
            {
                return NotFound();
            }

            var workPoster = _mapper.Map<WorkPoster>(workPosterUpdateDto);
            workPoster.Id = id; // Ensure the ID is set for the update operation
            var updatedPoster = await _workePosterService.UpdateWorkPosterAsync(id, workPoster);
            // var updatedPosterDto = _mapper.Map<WorkPosterResponseDto>(updatedPoster);
            return Ok(updatedPoster);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWorkPosterAsync(int id)
        {
            var existingPoster = await _workePosterService.GetWorkPosterByIdAsync(id);
            if (existingPoster == null)
            {
                return NotFound();
            }

            var result = await _workePosterService.DeleteWorkPosterAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }


    }


}