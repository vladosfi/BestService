﻿namespace BestService.Services.Data
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task<int> CreateAsync(string content, string userId, string companyId, byte rating);
    }
}