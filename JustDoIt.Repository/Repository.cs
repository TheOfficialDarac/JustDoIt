using JustDoIt.DAL;
using JustDoIt.Repository.Common;

namespace JustDoIt.Repository
{
    public class Repository: IRepository
    {
        #region Properties

        private readonly DataContext _context;
        #endregion Properties

        #region Constructor

        public Repository(DataContext context)
        {
            _context = context;
        }
        #endregion Constructor

        #region Methods

        public IEnumerable<Model.Task> GetTasks() => _context.Tasks.AsEnumerable();
        #endregion Methods
    }
}
