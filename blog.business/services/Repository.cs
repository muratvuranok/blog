namespace blog.business.services
{
    using System;
    using repositories;
    using data.models;
    using System.Collections.Generic;
    using data.context;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
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
        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
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
    }
}