using LabourChowk_webapi.Models;
using LabourChowk_webapi.Reporsitories.Interfaces;
using LabourChowk_webapi.Repositories;
using LabourChowk_webapi.Services.Interfaces;

namespace LabourChowk_webapi.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IGenericRepository<Worker> _repository;

        public WorkerService(IGenericRepository<Worker> repository)
        {
            _repository = repository;
        }

        public Task<List<Worker>> GetAllWorkersAsync() => _repository.GetAllAsync();
        public Task<Worker?> GetWorkerByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<Worker> AddWorkerAsync(Worker worker) => _repository.AddAsync(worker);
        public async Task<(Worker? worker, string? errorMessage)> AddWorkerWithValidationAsync(Worker worker)
        {
            bool isUnique = await _repository.IsUniqueAsync(w => w.Phone == worker.Phone);

            if (!isUnique)
                return (null, "Phone number is already in use.");
            await _repository.AddAsync(worker);
            return (worker, null);
        }

        public async Task<bool> UpdateWorkerAsync(int id, Worker updatedWorker)
        {
            var worker = await _repository.GetByIdAsync(id);
            if (worker == null) return false;

            // Example: maybe don’t update password here
            worker.Name = updatedWorker.Name;
            worker.Skill = updatedWorker.Skill;

            await _repository.UpdateAsync(worker);
            return true;
        }

        public async Task<bool> DeleteWorkerAsync(int id)
        {
            var worker = await _repository.GetByIdAsync(id);
            if (worker == null) return false;

            await _repository.DeleteAsync(worker);
            return true;
        }

    }
}
