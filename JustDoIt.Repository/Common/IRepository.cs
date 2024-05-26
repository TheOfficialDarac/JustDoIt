namespace JustDoIt.Repository.Common
{
    public interface IRepository
    {
        Task<IEnumerable<Model.Task>> GetTasks();
    }
}
