namespace blog.business.services
{
    using System;
    using repositories;
    using data.models;
    using System.Collections.Generic;
    using data.context;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Linq.Expressions;

    public class Repository<T> : IRepository<T> where T : CoreEntity
    {
        private readonly blogcontext _context;
        private DbSet<T> _entities;
        public Repository(blogcontext context)
        {
            this._context = context;
            this._entities = context.Set<T>();
        }

        public void Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _entities.Add(entity);
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                RollBack();
            }
        }
        public virtual void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Delete(Guid id)
        {
            if (id == null) throw new ArgumentNullException("entity");
            T entity = _entities.SingleOrDefault(s => s.Id == id);
            if (entity == null) throw new ArgumentNullException("entity");
            _entities.Remove(entity);
            _context.SaveChanges();
        }
        public T GetById(Guid id)
        {
            if (id == null) throw new ArgumentNullException("entity");
            return _entities.SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public void RollBack()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public bool Any(Expression<Func<T, bool>> exp)
        {
            return _entities.Any(exp);
        }

        public void Delete(T item)
        {
            _entities.Remove(item);
        }

        public void Delete(Expression<Func<T, bool>> exp)
        { 
            _entities.RemoveRange(GetDefault(exp));
        }

        public IEnumerable<T> GetDefault(Expression<Func<T, bool>> exp)
        {
            return _entities.Where(exp);
        }


        // public bool SaveChanges()
        // {
        //     return _context.SaveChanges() > 0 ? true : false;
        // }
    }
}