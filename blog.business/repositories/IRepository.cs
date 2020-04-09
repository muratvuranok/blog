namespace blog.business.repositories
{
    using System;
    using System.Collections.Generic;
    using data.models;
    public interface IRepository<T> where T : CoreEntity
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
        T GetById(Guid id);
        IEnumerable<T> GetAll();
    }
}