using Demo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Repository
{
    public interface IResultRepository
    {

        Task<Result> GetBIdAsync(int id);

        Task<List<Result>> ListAsync();

        Task AddAsync(Result result);
        Task<bool> updateAsync(Result result);

        List<Result> PageList(int pageindex, int pagesize, out int pagecount);

        Task<bool> DeleteAsync(Result result);
    }
}
