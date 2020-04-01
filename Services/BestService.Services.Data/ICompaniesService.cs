﻿namespace BestService.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BestService.Data.Models;

    public interface ICompaniesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Task<int> AddAsync(string name, string description, string image, string officialSite, ApplicationUser user, string categoryId);
    }
}