using ETicaretAPI.Domain.Entites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true);

        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);               //MIGHT BE ASYNC

        Task<T> GetSingle(Expression<Func<T, bool>> method, bool tracking = true);
        //Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);

        Task<T> GetbyIDAsync(string id, bool tracking = true);
    }
} 
