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

        public Task<string> Test() => Task.FromResult("This is a success");
        #endregion Methods
    }
}
