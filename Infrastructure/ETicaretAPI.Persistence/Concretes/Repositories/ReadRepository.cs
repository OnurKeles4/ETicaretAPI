using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entites.Common;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Concretes.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ETicaretAPIDbContext _context;
        
        public ReadRepository(ETicaretAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();

            if(!tracking) 
                query = query.AsNoTracking();
            
            return query;
        }


        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method).AsQueryable();

            if(!tracking) 
                query = query.AsNoTracking();
            return query;
        }

        
        public async Task<T> GetSingle(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
            
        }

        public async Task<T> GetbyIDAsync(string id, bool tracking = true)
        {
            
            var query = Table.AsQueryable();
            if (!tracking)
                query = Table.AsNoTracking();

            return await query.FirstOrDefaultAsync(data => data.id == Guid.Parse(id));        //two different ways to get an data by id
        }

        //public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        //{
        //    return await Table.FirstOrDefaultAsync(method);
        //}


    }
}
