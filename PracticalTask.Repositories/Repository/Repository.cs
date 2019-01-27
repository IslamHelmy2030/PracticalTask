using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PracticalTask.Repositories.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private const bool TrueExpression = true;
        protected readonly DbContext Context;
        protected DbSet<T> DbSet;
        public Repository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        public T Get(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public string GetKeyField(Type type)
        {
            var allProperties = type.GetProperties();

            var keyProperty = allProperties.SingleOrDefault(p => p.IsDefined(typeof(KeyAttribute)));

            return keyProperty?.Name;
        }

        public object GetKeyValue(T t)
        {
            var key =
                typeof(T).GetProperties().FirstOrDefault(
                    p => p.GetCustomAttributes(typeof(KeyAttribute), true).Length != 0);
            return key?.GetValue(t, null);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = DbSet.OfType<T>();
            query = includes.Aggregate(query, (current, property) => current.Include(property));
            return query.Where(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return Find(predicate, includes).FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return DbSet.AsQueryable();
        }
        public IQueryable<T> GetAll(int pageSize, int pageNumber)
        {
            return DbSet.OrderBy(GetKeyField(typeof(T))).Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            return Find(x => TrueExpression, includes);
        }

        public T Add(T newEntity)
        {
            return DbSet.Add(newEntity);
        }

        public IEnumerable<T> AddRange(IEnumerable<T> newEntities)
        {
            return DbSet.AddRange(newEntities);
        }

        public void Update(T entity)
        {
            var key = GetKeyValue(entity);

            Update(entity, key);

            //System.Data.Entity.Infrastructure.DbEntityEntry<T> entry = Context.Entry(entity);
            //if (entry.State == EntityState.Detached)
            //{
            //    DbSet.Attach(entity);
            //    entry = Context.Entry(entity);
            //}
            //entry.State = EntityState.Modified;
        }

        public void Update(T entity, object key)
        {
            var originalEntity = DbSet.Find(key);

            Update(originalEntity, entity);
        }

        public void Update(T originalEntity, T newEntity)
        {
            Context.Entry(originalEntity).CurrentValues.SetValues(newEntity);
        }

        public void AddOrUpdate(T entity)
        {
            Context.Set<T>().AddOrUpdate(entity);
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Remove(Expression<Func<T, bool>> predicate)
        {
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                var objects = Find(predicate);
                foreach (var obj in objects)
                {
                    DbSet.Remove(obj);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, int pageSize, int pageNumber)
        {
            return DbSet.Where(predicate).OrderBy(GetKeyField(typeof(T))).Skip((pageNumber - 1) * pageSize).Take(pageSize);

        }
    }
}
