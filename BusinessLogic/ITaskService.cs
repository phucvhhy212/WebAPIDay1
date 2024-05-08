using Task = DataAccess.Task;
namespace BusinessLogic
{
    public interface ITaskService
    {
        IEnumerable<Task> GetAll();
        Task? GetById(Guid id);
        void Delete(Guid id);
        void Edit(Guid id,Task task);
        void Add(Task task);
        void AddRange(IEnumerable<Task> task);
        void DeleteRange(IEnumerable<Guid> taskIds);
    }
}
