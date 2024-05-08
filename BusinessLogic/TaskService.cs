using Task = DataAccess.Task;

namespace BusinessLogic
{
    public class TaskService : ITaskService
    {
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
            return data;
        }

        public Task? GetById(Guid id)
        {
            return data.FirstOrDefault(x => x.Id == id);
        }

        public void Delete(Guid id)
        {
            var taskToDelete = GetById(id);
            if (taskToDelete != null)
            {
                data.Remove(taskToDelete);
            }
        }

        public void Edit(Guid id, Task task)
        {
            var taskToUpdate = GetById(id);
            if (taskToUpdate != null)
            {
                taskToUpdate.IsCompleted = task.IsCompleted;
                taskToUpdate.Title = task.Title;
            }

        }

        public void Add(Task task)
        {
            var findTask = GetById(task.Id);
            if (findTask == null)
            {
                data.Add(task);
            }
        }

        public void AddRange(IEnumerable<Task> task)
        {
            if (!data.Any(x => task.Select(t => t.Id).Contains(x.Id)))
            {
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
                    break;
                }
            }

            if (isValid)
            {
                foreach (var id in taskIds)
                {
                    Delete(id);
                }
            }
        }
    }
}
