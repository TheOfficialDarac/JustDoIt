using JustDoIt.DAL;
using JustDoIt.Model.Database;
using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Requests.Attachments;
using JustDoIt.Model.Responses.Attachments;
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

        public async Task<CreateAttachmentResponse> CreateTaskAttachment(CreateAttachmentRequest request)
        {
            var response = await Create(request);

            if (response.Id == 0) return new();

            var result = new TaskAttachment { TaskId = request.TypeId, AttachmentId = response.Id };
            _context.TaskAttachments.Add(result);
            await _context.SaveChangesAsync();

            return new CreateAttachmentResponse { Id = result.Id };
        }

        public async Task<CreateAttachmentResponse> CreateIssueAttachment(CreateAttachmentRequest request)
        {
            var response = await Create(request);

            if (response.Id == 0) return new();

            var result = new IssueAttachment { IssueId = request.TypeId, AttachmentId = response.Id };
            _context.IssueAttachments.Add(result);
            await _context.SaveChangesAsync();

            return new CreateAttachmentResponse { Id = result.Id };
        }

        public async Task<CreateAttachmentResponse> Create(CreateAttachmentRequest request)
        {
            try
            {
                var attachment = new Attachment();

                await _context.Attachments.AddAsync(attachment);
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

                return new CreateAttachmentResponse { Id = attachment.Id };
            }
            catch (Exception) { /* Logger */ }
            return new();
        }

        public async Task<AttachmentResponse> DeleteTaskAttachment(GetSingleItemRequest request)
        {
            var taskAttachment = await _context.TaskAttachments.FindAsync(request.Id);

            if (taskAttachment == null) return new() { TypeId = request.Id };

            var response = await Delete(new GetSingleItemRequest { Id = taskAttachment.Id });

            if (response.AttachmentId != 0) return new() { TypeId = request.Id, AttachmentId = response.AttachmentId };

            _context.TaskAttachments.Remove(taskAttachment);

            await _context.SaveChangesAsync();
            return new();
        }

        public async Task<AttachmentResponse> DeleteIssueAttachment(GetSingleItemRequest request)
        {
            try
            {
                var issueAttachment = await _context.IssueAttachments.FindAsync(request.Id);

                if (issueAttachment == null) return new() { TypeId = request.Id };

                var response = await Delete(new GetSingleItemRequest { Id = issueAttachment.Id });

                if (response.AttachmentId != 0) return new() { TypeId = request.Id, AttachmentId = response.AttachmentId };

                _context.IssueAttachments.Remove(issueAttachment);

                await _context.SaveChangesAsync();
                return new();

            }
            catch (Exception) { /* Logger */ }
            return new() { };
        }

        public async Task<AttachmentResponse> Delete(GetSingleItemRequest request)
        {
            try
            {
                var found = await _context.Attachments.FindAsync(request.Id);
                if (found != null)
                {
                    _context.Remove(found);
                    await _context.SaveChangesAsync();
                    return new();
                }

                return new() { AttachmentId = request.Id };
            }
            catch (Exception) { }
            return new() { AttachmentId = request.Id };
        }

        public async Task<IEnumerable<AttachmentResponse>> GetAll(GetAttachmentRequest request)
        {
            try
            {
                var query = _context.Attachments.AsQueryable();

                if (request.AttachmentId != 0)
                {
                    query = query.Where(x => x.Id == request.AttachmentId);
                }

                var result = await query.ToListAsync();
                return _mapper.ToResponseList(result);
            }
            catch (Exception) { }
            return [];
        }

        public async Task<IEnumerable<AttachmentResponse>> GetAllTaskAttachments(GetSingleItemRequest request)
        {
            try
            {
                var result = await _context.TaskAttachments.Where(x => x.TaskId == request.Id).Select(x => x.Attachment).ToListAsync();
                return _mapper.ToResponseList(result);
            }
            catch (Exception) { /* Logger */ }
            return [];
        }

        public async Task<IEnumerable<AttachmentResponse>> GetAllIssueAttachments(GetSingleItemRequest request)
        {
            try
            {
                var result = await _context.IssueAttachments.Where(x => x.IssueId == request.Id).Select(x => x.Attachment).ToListAsync();
                return _mapper.ToResponseList(result);
            }
            catch (Exception) { /* Logger */ }
            return [];
        }

        public async Task<AttachmentResponse> GetSingle(GetSingleItemRequest request)
        {
            try
            {
                var result = await _context.Attachments.FindAsync(request.Id);

                return result is null ? new() : _mapper.ToResponse(result);
            }
            catch (Exception) { }
            return new();
        }

        //public async Task<AttachmentResponse> Update(UpdateAttachmentRequest request)
        //{
        //    try
        //    {
        //        var attachment = await _context.TaskAttachments.FindAsync(request.Id);
        //        if (attachment is null) return new();

        //        attachment.Filepath = request.FilePath;
        //        if (request.Attachment != null && request.Attachment.Length > 0 && string.IsNullOrEmpty(request.FilePath))
        //        {
        //            var ext = Path.GetExtension(request.Attachment.FileName).ToLowerInvariant();
        //            var filePath = $"{Directory.GetCurrentDirectory()}\\Assets\\Attachments\\{attachment.Id}{ext}";

        //            using (var stream = File.Create(filePath))
        //            {
        //                await request.Attachment.CopyToAsync(stream);
        //            }
        //            attachment.Filepath = (filePath).Replace("\\", "/");
        //        }

        //        attachment.TaskId = request.TaskId;

        //        await _context.SaveChangesAsync();
        //        return _mapper.ToResponse(attachment);
        //    }
        //    catch (Exception e) { }
        //    return new();
        //}

        public async Task<AttachmentResponse> UpdateTaskAttachments(UpdateAttachmentsRequest request)
        {
            try
            {
                //delete old attacgemts
                var taskAttachments = await _context.TaskAttachments.Where(x => x.TaskId == request.TypeId).ToListAsync();
                if (taskAttachments.Count > 0)
                {

                    taskAttachments.ForEach(ta =>
                    {
                        if (!string.IsNullOrEmpty(ta.Attachment.Filepath))
                            File.Delete(ta.Attachment.Filepath);

                        _context.Attachments.Remove(ta.Attachment);
                        _context.TaskAttachments.Remove(ta);
                    });
                    await _context.SaveChangesAsync();
                }

                // create the new ones
                if (request.Attachments.Any())
                {
                    foreach (var formFile in request.Attachments)
                    {
                        if (formFile.Length > 0)
                        {
                            await CreateTaskAttachment(new CreateAttachmentRequest() { TypeId = request.TypeId, Attachment = formFile });
                        }
                    }
                }
                return new();
            }
            catch (Exception) { /* Logger */ }
            return new() { TypeId = request.TypeId };
        }

        public async Task<AttachmentResponse> UpdateIssueAttachments(UpdateAttachmentsRequest request)
        {
            try
            {
                //delete old attacgemts
                var issueAttachments = await _context.IssueAttachments.Where(x => x.IssueId == request.TypeId).ToListAsync();
                if (issueAttachments.Count > 0)
                {

                    issueAttachments.ForEach(ta =>
                    {
                        if (!string.IsNullOrEmpty(ta.Attachment.Filepath))
                            File.Delete(ta.Attachment.Filepath);

                        _context.Attachments.Remove(ta.Attachment);
                        _context.IssueAttachments.Remove(ta);
                    });
                    await _context.SaveChangesAsync();
                }

                // create the new ones
                if (request.Attachments.Any())
                {
                    foreach (var formFile in request.Attachments)
                    {
                        if (formFile.Length > 0)
                        {
                            await CreateIssueAttachment(new CreateAttachmentRequest() { TypeId = request.TypeId, Attachment = formFile });
                        }
                    }
                }
                return new();
            }
            catch (Exception) { /* Logger */ }
            return new() { TypeId = request.TypeId };
        }

        public async Task<AttachmentResponse> Update(UpdateAttachmentsRequest request)
        {
            return null;
            //var toBeRemoved = await _context.Attachments.Where(x => x.TaskId == request.TaskId).ToListAsync();
            //_context.TaskAttachments.RemoveRange(toBeRemoved);
            //await _context.SaveChangesAsync();

            //if (request.Attachments.Any())
            //{
            //    foreach (var formFile in request.Attachments)
            //    {
            //        if (formFile.Length > 0)
            //        {
            //            var ext = Path.GetExtension(formFile.FileName).ToLowerInvariant();
            //            var filePath = $"{Directory.GetCurrentDirectory()}\\Assets\\Attachments\\{formFile.FileName}";

            //            using (var stream = File.Create(filePath))
            //            {
            //                await formFile.CopyToAsync(stream);
            //            }
            //            var file = new TaskAttachment
            //            {
            //                TaskId = request.TaskId,
            //                Filepath = (filePath).Replace("\\", "/")
            //            };

            //            _context.TaskAttachments.Add(file);
            //        }
            //    }
            //}

            //await _context.SaveChangesAsync();
            //return true;
        }
        #endregion
    }
}
