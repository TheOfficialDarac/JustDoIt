using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Responses;
using JustDoIt.Model.Responses.Abstractions;

namespace JustDoIt.Service.Abstractions.Common
{
    public interface IGenericService<TResponse, TCreateRequest, TCreateResponse, TGetRequest, TGetSingleRequest, TUpdateRequest>
        where TResponse : Response
        where TCreateRequest : CreateRequest
        where TGetRequest : GetRequest
        where TGetSingleRequest : GetSingleRequest
        where TUpdateRequest : UpdateRequest
        where TCreateResponse : CreateResponse
    {
        Task<RequestResponse<TCreateResponse>> Create(TCreateRequest request);
        Task<RequestResponse<TResponse>> GetAll(TGetRequest request);
        Task<RequestResponse<TResponse>> GetSingle(TGetSingleRequest request);
        Task<RequestResponse<TResponse>> Update(TUpdateRequest request);
        Task<RequestResponse<TResponse>> Delete(TGetSingleRequest request);
    }
}
