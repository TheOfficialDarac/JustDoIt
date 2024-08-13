using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Repository.Common;
using JustDoIt.Service.Common;

namespace JustDoIt.Service
{
    class TaskService : ITaskService, IGenericService<TaskDTO>
    {
        #region Properties

        private ITask _repository { get; set; }
        #endregion

        public TaskService(IRepository repository)
        {
            _repository = repository;
        }

        #region Methods

        public Tuple<Task<IEnumerable<Model.Task>>, IEnumerable<ErrorMessage>> GetUserTasks(string userID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskDTO>> GetAll(string? title, string? description, string? pictureURL, DateTime? deadlineStart, DateTime? deadlineEnd, string? state, string? adminID, int? projectID)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<bool, IEnumerable<ErrorMessage>>> Create(TaskDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<IEnumerable<TaskDTO>, IEnumerable<ErrorMessage>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<TaskDTO, IEnumerable<ErrorMessage>>> GetSingle(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<bool, IEnumerable<ErrorMessage>>> Update(TaskDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<bool, IEnumerable<ErrorMessage>>> Delete(TaskDTO entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
