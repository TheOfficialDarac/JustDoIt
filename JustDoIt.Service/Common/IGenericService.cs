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
        //Tuple<Task<bool>, IEnumerable<ErrorMessage>> Create(T entity);
        //Tuple<Task<IEnumerable<T>>, IEnumerable<ErrorMessage>> GetAll();
        //Tuple<Task<T>, IEnumerable<ErrorMessage>> GetSingle(string id);
        //Tuple<Task<bool>, IEnumerable<ErrorMessage>> Update(T entity);
        //Tuple<Task<bool>, IEnumerable<ErrorMessage>> Delete(T entity);

        Task<Tuple<bool, IEnumerable<ErrorMessage>>> Create(T entity);
        Task<Tuple<IEnumerable<T>, IEnumerable<ErrorMessage>>> GetAll();
        Task<Tuple<T, IEnumerable<ErrorMessage>>> GetSingle(string id);
        Task<Tuple<bool, IEnumerable<ErrorMessage>>> Update(T entity);
        Task<Tuple<bool, IEnumerable<ErrorMessage>>> Delete(T entity);
    }
}
