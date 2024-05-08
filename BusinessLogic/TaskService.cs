using Microsoft.Extensions.Logging;
using Task = DataAccess.Task;

namespace BusinessLogic
{
    public class TaskService : ITaskService
    {
        private readonly ILogger<TaskService> _logger;
        public TaskService(ILogger<TaskService> logger)
        {
            _logger = logger;
        }
        private static List<Task> TaskData()
        {
            return new List<Task>
            {
                new Task
                {
                    IsCompleted = true,
                    Title = "Initial Task"
                }
            };
        }

        private static List<Task> data = TaskData();
        public IEnumerable<Task> GetAll()
        {
            _logger.Log(LogLevel.Information,"Getting all tasks");
            return data;
        }

        public Task? GetById(Guid id)
        {
            _logger.Log(LogLevel.Information, $"Getting task with id = {id}");
            return data.FirstOrDefault(x => x.Id == id);
        }

        public void Delete(Guid id)
        {
            _logger.Log(LogLevel.Information, $"Finding task with id = {id} to delete");
            var taskToDelete = GetById(id);
            if (taskToDelete != null)
            {
                _logger.Log(LogLevel.Information, $"Deleting task with id = {id}");
                data.Remove(taskToDelete);
            }
            else
            {
                _logger.Log(LogLevel.Information, $"Task {id} not exist to delete");
                throw new Exception($"Task {id} not exist");
            }
        }

        public void Edit(Guid id, Task task)
        {
            _logger.Log(LogLevel.Information, $"Finding task with id = {id} to edit");
            var taskToUpdate = GetById(id);
            if (taskToUpdate != null)
            {
                _logger.Log(LogLevel.Information, $"Updating task with id = {id}");
                taskToUpdate.IsCompleted = task.IsCompleted;
                taskToUpdate.Title = task.Title;
            }
            else
            {
                _logger.Log(LogLevel.Information, $"Task {id} not exist to edit");
                throw new Exception($"Task {id} not exist");
            }

        }

        public void Add(Task task)
        {
            _logger.Log(LogLevel.Information, $"Adding task {task.Id}");
            data.Add(task);
        }

        public void AddRange(IEnumerable<Task> task)
        {
            if (!data.Any(x => task.Select(t => t.Id).Contains(x.Id)))
            {
                _logger.Log(LogLevel.Information, $"Bulk add tasks");
                data.AddRange(task);
            }
        }

        public void DeleteRange(IEnumerable<Guid> taskIds)
        {
            bool isValid = true;
            foreach (var id in taskIds)
            {
                if (GetById(id) == null)
                {
                    isValid = false;
                    _logger.Log(LogLevel.Error, $"Task {id} exist");
                    throw new Exception($"Task {id} not exist");
                }
            }

            if (isValid)
            {
                _logger.Log(LogLevel.Information, "All task exist to delete");
                _logger.Log(LogLevel.Information, "Bulk delete task");
                foreach (var id in taskIds)
                {
                    Delete(id);
                }
            }
        }
    }
}
