using LabourChowk_webapi.Models;
using LabourChowk_webapi.Reporsitories.Interfaces;
using LabourChowk_webapi.Repositories;
using LabourChowk_webapi.Services.Interfaces;

namespace LabourChowk_webapi.Services
{
    public class WorkPosterService : IWorkPosterService
    {
        private readonly IGenericRepository<WorkPoster> _repository;

        public WorkPosterService(IGenericRepository<WorkPoster> repository)
        {
            _repository = repository;
        }

        public Task<List<WorkPoster>> GetAllWorkPostersAsync() => _repository.GetAllAsync();
        public Task<WorkPoster?> GetWorkPosterByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<WorkPoster> CreateWorkPosterAsync(WorkPoster poster) => _repository.AddAsync(poster);

        public async Task<bool> UpdateWorkPosterAsync(int id, WorkPoster updatedPoster)
        {
            var poster = await _repository.GetByIdAsync(id);
            if (poster == null) return false;

            poster.Name = updatedPoster.Name;
            poster.Phone = updatedPoster.Phone;

            await _repository.UpdateAsync(poster);
            return true;
        }

        public async Task<bool> DeleteWorkPosterAsync(int id)
        {
            var poster = await _repository.GetByIdAsync(id);
            if (poster == null) return false;

            await _repository.DeleteAsync(poster);
            return true;
        }


    }
}
