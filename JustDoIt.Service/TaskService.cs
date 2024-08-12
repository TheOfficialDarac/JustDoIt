using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Repository.Common;
using JustDoIt.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Service
{
    class TaskService : ITaskService, IGenericService<TaskDTO>
    {
        #region Properties

        private IRepository _repository { get; set; }
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

        public Task<Tuple<bool, IEnumerable<ErrorMessage>>> Create(TaskDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<IEnumerable<TaskDTO>, IEnumerable<ErrorMessage>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<TaskDTO, IEnumerable<ErrorMessage>>> GetSingle(string id)
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
