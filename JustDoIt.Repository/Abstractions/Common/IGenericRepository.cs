using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Responses.Abstractions;

namespace JustDoIt.Repository.Abstractions.Common
{
    public interface IGenericRepository<TResponse, TCreateRequest, TCreateResponse, TGetRequest, TGetSingleRequest, TUpdateRequest>
        where TResponse : Response
        where TCreateRequest : CreateRequest
        where TGetRequest : GetRequest
        where TGetSingleRequest : GetSingleRequest
        where TUpdateRequest : UpdateRequest
        where TCreateResponse : CreateResponse
    {
        Task<TCreateResponse> Create(TCreateRequest request);
        Task<IEnumerable<TResponse>> GetAll(TGetRequest request);
        Task<TResponse> GetSingle(TGetSingleRequest request);
        Task<TResponse> Update(TUpdateRequest request);
        Task<TResponse> Delete(TGetSingleRequest request);
    }
}
