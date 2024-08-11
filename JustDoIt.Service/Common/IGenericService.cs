using JustDoIt.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Service.Common
{
    interface IGenericService<T> where T : class
    {
        Tuple<Task<IEnumerable<T>>, IEnumerable<ErrorMessage>> GetAll();
        Tuple<Task<T>, IEnumerable<ErrorMessage>> GetSingle(string id);
        Tuple<Task<Boolean>, IEnumerable<ErrorMessage>> Create(T entity);
        Tuple<Task<Boolean>, IEnumerable<ErrorMessage>> Update(T entity);
        Tuple<Task<Boolean>, IEnumerable<ErrorMessage>> Delete(T entity);
    }
}
