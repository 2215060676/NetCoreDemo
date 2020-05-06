using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Model;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repository
{
    public class ResultTypeRepository : IResultTypeRepository
    {
        private  DemoContext DemoContext;

        public ResultTypeRepository(DemoContext _demoContext)
        {
            DemoContext = _demoContext;
        }
        public Task<List<ResultType>> typeListAsync()
        {
            return DemoContext.ResultType.ToListAsync();
        }
    }
}
