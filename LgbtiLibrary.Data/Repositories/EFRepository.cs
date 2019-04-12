using LgbtiLibrary.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgbtiLibrary.Data.Repositories
{
    public class EFRepository<T> : IEFRepository<T> where T : class
    {
        private readonly ILgbtiLibraryDb context;

        private IDbSet<T> DbSet { get; set; }

        public EFRepository(ILgbtiLibraryDb context)
        {
            this.context = context;
            this.DbSet = this.context.Set<T>();
        }

        public T GetById(Guid id)
        {
            return this.DbSet.Find(id);
        }

        public IQueryable<T> All {
            get {
                return this.context.Set<T>();
            }
        }

        public void Add(T entity)
        {
            DbEntityEntry entry = this.context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }

        public void Delete(T entity)
        {
            this.DbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = this.context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
