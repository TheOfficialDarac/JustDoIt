using JustDoIt.Model.Database;
using JustDoIt.Model.Responses.Utils;
using Riok.Mapperly.Abstractions;

namespace JustDoIt.Repository.Mappers
{
    [Mapper]
    public partial class UtilsMapper
    {
        //public partial CategoryResponse ToCategoryResponse(Category item);
        public partial IEnumerable<CategoryResponse> ToCategoryResponseList(IEnumerable<Category> items);

        public partial StatusResponse ToStatusResponse(Status item);
        public partial IEnumerable<StatusResponse> ToStatusResponseList(IEnumerable<Status> items); 
        public partial StateResponse ToStateResponse(State item);
        public partial IEnumerable<StateResponse> ToStateResponseList(IEnumerable<State> items);

    }
}
