using JustDoIt.DAL;
using JustDoIt.Model;
using JustDoIt.Model.Database;
using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Requests.Attachments;
using JustDoIt.Model.Responses.Attachments;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Repository.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

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

                if (request.Attachment != null && request.Attachment.Length > 0)
                {
                    var ext = Path.GetExtension(request.Attachment.FileName).ToLowerInvariant();
                    var filePath = $"{Directory.GetCurrentDirectory()}\\Assets\\Attachments\\{attachment.Id}{ext}";

                    using (var stream = File.Create(filePath))
                    {
                        await request.Attachment.CopyToAsync(stream);
                    }
                    attachment.Filepath = (filePath).Replace("\\", "/");
                }
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
                    query = query.Where(x => x.Id == request.AttachmentId);
                }

                if (request.TaskId != 0)
                {
                    query = query.Where(x => x.TaskId == request.TaskId);
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
                var attachment = await _context.TaskAttachments.FindAsync(request.Id);
                if (attachment is null) return new ();

                attachment.Filepath = request.FilePath;
                if (request.Attachment != null && request.Attachment.Length > 0 && string.IsNullOrEmpty(request.FilePath))
                {
                    var ext = Path.GetExtension(request.Attachment.FileName).ToLowerInvariant();
                    var filePath = $"{Directory.GetCurrentDirectory()}\\Assets\\Attachments\\{attachment.Id}{ext}";

                    using (var stream = File.Create(filePath))
                    {
                        await request.Attachment.CopyToAsync(stream);
                    }
                    attachment.Filepath = (filePath).Replace("\\", "/");
                }

                attachment.TaskId = request.TaskId;

                await _context.SaveChangesAsync();
                return _mapper.ToResponse(attachment);
            }
            catch (Exception e) { }
            return new();
        }

        public async Task<bool> UpdateTaskAttachments(UpdateTaskAttachmentsRequest request)
        {
            var toBeRemoved = await _context.TaskAttachments.Where(x => x.TaskId == request.TaskId).ToListAsync();
            _context.TaskAttachments.RemoveRange(toBeRemoved);
            await _context.SaveChangesAsync();

            if(request.Attachments != null)
            foreach (var formFile in request.Attachments)
            {
                if (formFile.Length > 0)
                {
                    var ext = Path.GetExtension(formFile.FileName).ToLowerInvariant();
                    var filePath = $"{Directory.GetCurrentDirectory()}\\Assets\\Attachments\\{formFile.FileName}";

                    using (var stream = File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    var file = new TaskAttachment
                    {
                        TaskId = request.TaskId,
                        Filepath = (filePath).Replace("\\", "/")
                    };

                    _context.TaskAttachments.Add(file);
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
