﻿namespace JustDoIt.Service.Common
{
    public interface IService
    {
        Task<string> Test();
        Task<IEnumerable<Model.Task>> GetTasks();
    }
}
