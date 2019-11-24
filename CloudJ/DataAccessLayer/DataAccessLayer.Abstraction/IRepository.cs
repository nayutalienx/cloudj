using DataAccessLayer.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstraction
{
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Получить все
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<T>> GetAllAsync();
        /// <summary>
        /// Получить по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(long id);
        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);
        /// <summary>
        /// Обновить 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity);
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task RemoveAsync(T entity);
        /// <summary>
        /// Сохранить
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
