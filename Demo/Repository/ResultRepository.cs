using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Model;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repository
{
    public class ResultRepository : IResultRepository
    {

        private readonly DemoContext DemoContext;

        public ResultRepository(DemoContext _demoContext)
        {
            DemoContext = _demoContext;
        }

        public Task AddAsync(Result result)
        {
            DemoContext.Results.Add(result);
            return DemoContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Result result)
        {
            DemoContext.Entry<Result>(result).State = EntityState.Deleted;
            return await DemoContext.SaveChangesAsync() > 0;
        }

        public Task<Result> GetBIdAsync(int id)
        {
            return DemoContext.Results.Include(r => r.type).FirstOrDefaultAsync(r => r.id == id);
        }

        public Task<List<Result>> ListAsync()
        {
            return DemoContext.Results.Include<Result, ResultType>(r => r.type).ToListAsync();
        }

        public List<Result> PageList(int pageindex, int pagesize, out int pagecount)
        {
            if (pageindex >= 1)
            {
                //AsQueryable  不直接添加到内存，做一下延迟的
                var quety = DemoContext.Results.Include<Result, ResultType>(r => r.type).AsQueryable();
                var count = quety.Count();
                pagecount = count % pagesize == 0 ? count % pagesize : count % pagesize + 1;
                var results = quety.OrderByDescending(r => r.Create).Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
                return results;
            }
            else
            {
                var quety = DemoContext.Results.Include<Result, ResultType>(r => r.type).AsQueryable();
                var count = quety.Count();
                pagecount = count % pagesize == 0 ? count % pagesize : count % pagesize + 1;
                var results = quety.OrderByDescending(r => r.Create).Skip((1) * pagesize).Take(pagesize).ToList();
                return results;
            }
           
        }

        public async Task<bool> updateAsync(Result result)
        {
            DemoContext.Results.Update(result);
            return await DemoContext.SaveChangesAsync() > 0;
        }
   
    }
}
