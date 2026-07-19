using Microsoft.EntityFrameworkCore;
using AttendanceControl.Domain.Core;
using AttendanceControl.Infrastructure.Context;

namespace AttendanceControl.Infrastructure.Core
{
    public class BaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        public virtual T? GetById(int id)
        {
            return _entities.Find(id);
        }

        public virtual T Create(T entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual void Delete(int id)
        {
            var entity = _entities.Find(id);
            if (entity != null)
            {
                _entities.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}