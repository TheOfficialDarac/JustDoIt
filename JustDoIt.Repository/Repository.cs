using JustDoIt.DAL;
using JustDoIt.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace JustDoIt.Repository
{
    public class Repository : IRepository
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

        public async Task<IEnumerable<Model.Task>> GetTasks()
        {
            List<Model.Task> tasks;
            try {
                tasks = await _context.Tasks.ToListAsync();
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
            return tasks;
        }
        #endregion Methods
    }
}
