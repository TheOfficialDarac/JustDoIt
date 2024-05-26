namespace JustDoIt.Repository.Common
{
    public interface IRepository
    {
        IEnumerable<Model.Task> GetTasks();
    }
}
