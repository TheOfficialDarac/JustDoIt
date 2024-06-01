using JustDoIt.Model;
using JustDoIt.Repository.Common;
using JustDoIt.Service.Common;

namespace JustDoIt.Service
{
    public class Service: IService
    {
        #region Properties

        private readonly IRepository _repository;
        #endregion Properties

        #region Constructors

        public Service(IRepository repository)
        {
            _repository = repository;
        }

        #endregion Constructors

        #region Methods

        public async Task<Model.Task> GetTask(int id)
        {
            return await _repository.GetTask(id);
        }

        public async Task<IEnumerable<Model.Task>> GetTasks(
            string? title,
            string? description,
            string? pictureURL,
            DateTime? deadlineStart,
            DateTime? deadlineEnd,
            string? state,
            int? adminID,
            int? projectID,
            int page = 1,
            int pageSize = 5
        ) {
            return await _repository.GetTasks(
                title:title,
                description: description,
                pictureURL:pictureURL,
                deadlineStart:deadlineStart,
                deadlineEnd:deadlineEnd,
                state:state,
                adminID:adminID,
                projectID:projectID,
                page:page,
                pageSize:pageSize
                );
        }

        public async Task<bool> UpdateTask(Model.Task task)
        {
            return await _repository.PutTask(task);
        }

         public async Task<bool> CreateTask(Model.Task task)
        {
            return await _repository.CreateTask(task);
        }

        public async Task<bool> DeleteTask(Model.Task task)
        {
            return await _repository.DeleteTask(task);
        }

        public async Task<IEnumerable<Project>> GetProjects(
            string? title,
            string? description,
            string? pictureURL,
            int? adminID,
            int page = 1,
            int pageSize = 5
            ) {
            return await _repository.GetProjects(
                title:title,
                description: description,
                pictureURL: pictureURL,
                adminID:adminID,
                page:page,
                pageSize: pageSize
            );
        }

        #endregion Methods
    }
}
