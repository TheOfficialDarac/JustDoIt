using JustDoIt.Common;

namespace JustDoIt.Model.DTOs.Responses
{
    public class RequestResponse<T> where T : class
    {
        public readonly Result Result;
        public readonly IEnumerable<T>? ListOfData;
        public readonly T? Data;

        public RequestResponse(T data, Result result)
        {
            Data = data;
            Result = result;
        }

        public RequestResponse(IEnumerable<T> listOfData, Result result)
        {
            Result = result;
            ListOfData = listOfData;
        }
    }
}
