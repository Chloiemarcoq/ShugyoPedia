using ShugyopediaApp.Data;
using ShugyopediaApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Basecode.Data.Repositories
{
    public class BaseRepository
    {
        protected IUnitOfWork UnitOfWork { get; set; }

        protected shugyopediaDBContext Context => (shugyopediaDBContext)UnitOfWork.Database;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));
            UnitOfWork = unitOfWork;
        }

        protected virtual DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }

        protected virtual void SetEntityState(object entity, EntityState entityState)
        {
            Context.Entry(entity).State = entityState;
        }
    }
}
