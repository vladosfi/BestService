namespace BestService.Services.Data
{
    using System.Threading.Tasks;

    public interface ISubscribesService
    {
        Task Subscribe(string email, string ip);

        Task<int> GetCountAsync();
    }
}
