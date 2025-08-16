using LabourChowk_webapi.Models;
using LabourChowk_webapi.Repositories;

namespace LabourChowk_webapi.Services
{
    public class WorkerService
    {
        private readonly GenericRepository<Worker> _repository;

        public WorkerService(GenericRepository<Worker> repository)
        {
            _repository = repository;
        }

        public Task<List<Worker>> GetAllWorkersAsync() => _repository.GetAllAsync();
        public Task<Worker?> GetWorkerByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<Worker> AddWorkerAsync(Worker worker) => _repository.AddAsync(worker);

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
