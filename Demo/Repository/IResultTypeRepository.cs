using Demo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Repository
{
     public interface IResultTypeRepository
    {
        Task<List<ResultType>> typeListAsync();
    }
}
