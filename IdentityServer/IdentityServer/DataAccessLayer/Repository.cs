using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.DataAccessLayer
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IQueryable<T>> GetAllAsync();

        Task<T> GetAsync(long id);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task RemoveAsync(T entity);

        Task SaveChangesAsync();

    }
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly IdentityContext Context;
        protected readonly DbSet<T> Entity;
        public Repository(IdentityContext context)
        {
            Context = context;
            Entity = Context.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            await Entity.AddAsync(entity);
            return entity;
        }

        public async Task<T> GetAsync(long id)
        {
            var e = await Entity.FindAsync(id);
            return e;
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return Entity;
        }

        public async Task RemoveAsync(T entity)
        {
            Entity.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            Entity.Update(entity);
            return entity;
        }
    }
}
