using DataAccessLayer.Abstraction;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly CloudjContext Context;
        protected readonly DbSet<T> Entity;
        public BaseRepository(CloudjContext context)
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
