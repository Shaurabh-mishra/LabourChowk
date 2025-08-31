using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabourChowk_webapi.DTOs;
using LabourChowk_webapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabourChowk_webapi.Services.Interfaces
{
    public interface IWorkerService
    {
        public Task<List<Worker>> GetAllWorkersAsync();
        public Task<Worker?> GetWorkerByIdAsync(int id);
        public Task<Worker> AddWorkerAsync(Worker worker);
        public Task<bool> UpdateWorkerAsync(int id, Worker updatedWorker);
        public Task<bool> DeleteWorkerAsync(int id);
        public Task<(Worker? worker, string? errorMessage)> AddWorkerWithValidationAsync(Worker worker);
    }
}