namespace BestService.Services.Data
{
    using System.Threading.Tasks;

    using BestService.Data.Common.Repositories;
    using BestService.Data.Models;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepo;

        public CommentsService(IDeletableEntityRepository<Comment> commentRepo)
        {
            this.commentRepo = commentRepo;
        }

        public async Task<int> CreateAsync(string content, string userId, string companyId, byte rating)
        {
            var comment = new Comment
            {
                Content = content,
                Rating = rating,
                CompanyId = companyId,
                UserId = userId,
            };

            await this.commentRepo.AddAsync(comment);
            await this.commentRepo.SaveChangesAsync();
            return comment.Id;
        }
    }
}
