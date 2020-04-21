namespace BestService.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data.Common.Repositories;
    using BestService.Data.Models;
    using BestService.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepo;

        public CommentsService(IDeletableEntityRepository<Comment> commentRepo)
        {
            this.commentRepo = commentRepo;
        }

        public async Task<int> CreateAsync(string content, string userId, int companyId, byte rating, int? parrentId = null)
        {
            var comment = new Comment
            {
                Content = content,
                Rating = rating,
                CompanyId = companyId,
                UserId = userId,
                ParrentId = parrentId,
            };

            await this.commentRepo.AddAsync(comment);
            await this.commentRepo.SaveChangesAsync();
            return comment.Id;
        }

        public async Task<int> GetCountAsync() => await this.commentRepo.AllAsNoTracking().CountAsync();

        public bool IsInCompanyId(int commentId, int companyId)
        {
            var commentCompanyId = this.commentRepo.All()
                .Where(x => x.Id == commentId)
                .Select(x => x.CompanyId)
                .FirstOrDefault();

            return commentCompanyId == companyId;
        }

        public IEnumerable<T> Get<T>(int? count = null)
        {
            IQueryable<Comment> companies = this.commentRepo
                .All()
                .OrderByDescending(x => x.CreatedOn);

            if (count.HasValue)
            {
                companies = companies.Take(count.Value);
            }

            return companies.To<T>().ToList();
        }
    }
}
