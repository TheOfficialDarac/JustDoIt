using System.Runtime.CompilerServices;
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

        #region Tasks

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

        #endregion Tasks

        #region Projects

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

        
        public async Task<Project> GetProject(int id)
        {
            return await _repository.GetProject(id);
        }

        public async Task<bool> UpdateProject(Project project)
        {
            return await _repository.UpdateProject(project);
        }

        public async Task<bool> DeleteProject(Project project)
        {
            return await _repository.DeleteProject(project);
        }

        public async Task<bool> CreateProject(Project project)
        {
            return await _repository.CreateProject(project);
        }
        #endregion Projects

        #region Users

        public async Task<IEnumerable<AppUser>> GetUsers(
            string? username, 
            string? firstName, 
            string? lastName, 
            string? email, 
            string? pictureURL, 
            int page = 1, 
            int pageSize = 5) {
            
            return await _repository.GetUsers(
                username:username,
                firstName:firstName,
                lastName:lastName,
                email:email,
                pictureURL:pictureURL,
                page:page,
                pageSize:pageSize
            );
        }

        public async Task<AppUser> GetUser(int id)
        {
            return await _repository.GetUser(id);
        }

        public async Task<bool> UpdateUser(AppUser user)
        {
            return await _repository.UpdateUser(user);
        }

        public async Task<bool> DeleteUser(AppUser user)
        {
            return await _repository.DeleteUser(user);
        }

        public async Task<bool> CreateUser(AppUser user)
        {
            return await _repository.CreateUser(user);
        }

        #endregion Users
        #endregion Methods
    }
}
