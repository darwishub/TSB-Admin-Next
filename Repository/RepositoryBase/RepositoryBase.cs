using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected InvesteurContext _context { get; set; }
        public RepositoryBase(InvesteurContext context)
        {
            _context = context;
        }
        public IQueryable<T> GetAll() => this._context.Set<T>().AsNoTracking();
        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression) =>
            this._context.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => this._context.Set<T>().Add(entity);
        public void Update(T entity) => this._context.Set<T>().Update(entity);
        public void Delete(T entity) => this._context.Set<T>().Remove(entity);

    }
}
