using JustDoIt.DAL;
using JustDoIt.Model.DTOs.Requests.Abstractions;
using JustDoIt.Model.DTOs.Requests.Attachments;
using JustDoIt.Model.DTOs.Responses.Attachments;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Repository.Mappers;
using Microsoft.EntityFrameworkCore;

namespace JustDoIt.Repository.Implementations
{
    public class AttachmentRepository(ApplicationContext context) : IAttachmentRepository
    {
        #region Properties

        private readonly ApplicationContext _context = context;
        private readonly AttachmentMapper _mapper = new();
        #endregion

        #region Methods

        public async Task<CreateAttachmentResponse> Create(CreateAttachmentRequest request)
        {
            try
            {
                var attachment = _mapper.CreateRequestToType(request);

                await _context.TaskAttachments.AddAsync(attachment);
                await _context.SaveChangesAsync();

                return _mapper.TypeToCreateResponse(attachment);
            }
            catch (Exception e) { /* Logger */ }
            return new();
        }

        public async Task<AttachmentResponse> Delete(GetSingleItemRequest request)
        {
            try
            {
                var found = await _context.TaskAttachments.FindAsync(request.Id);
                if (found != null)
                {
                    _context.Remove(found);
                    await _context.SaveChangesAsync();
                    return new();
                }

                return new AttachmentResponse { Id = request.Id };
            }
            catch (Exception e) { }
            return new AttachmentResponse { Id = request.Id };
        }

        public async Task<IEnumerable<AttachmentResponse>> GetAll(GetAttachmentRequest request)
        {
            try
            {
                var query = _context.TaskAttachments.AsQueryable();

                if (request.AttachmentId != 0)
                {
                    query = query.Where(x => x.Id.Equals(request.AttachmentId));
                }

                if (request.TaskId != 0)
                {
                    query = query.Where(x => x.Id.Equals(request.AttachmentId));
                }

                var result = await query.ToListAsync();
                return _mapper.ToResponseList(result);
            }
            catch (Exception e) { }
            return [];
        }

        public async Task<AttachmentResponse> GetSingle(GetSingleItemRequest request)
        {
            try
            {
                var result = await _context.TaskAttachments.FindAsync(request.Id);

                return result is null ? new() : _mapper.ToResponse(result);
            }
            catch (Exception e) { }
            return new();
        }

        public async Task<AttachmentResponse> Update(UpdateAttachmentRequest request)
        {
            try
            {
                var found = await _context.TaskAttachments.FindAsync(request.Id);
                if (found is null) return new ();

                found.Filepath = request.FilePath;
                found.TaskId = request.TaskId;

                await _context.SaveChangesAsync();
                return _mapper.ToResponse(found);
            }
            catch (Exception e) { }
            return new();
        }
        #endregion
    }
}
