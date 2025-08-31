using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabourChowk_webapi.Models;

namespace LabourChowk_webapi.Services.Interfaces
{
    public interface IWorkPosterService
    {
        public Task<List<WorkPoster>> GetAllWorkPostersAsync();
        public Task<WorkPoster?> GetWorkPosterByIdAsync(int id);
        public Task<WorkPoster> CreateWorkPosterAsync(WorkPoster workPoster);
        public Task<bool> UpdateWorkPosterAsync(int id, WorkPoster updatedPoster);
        public Task<bool> DeleteWorkPosterAsync(int id);
    }
}