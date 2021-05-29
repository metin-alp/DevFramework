using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DevFramework.Core.DataAccess;
using DevFramework.Core.Entities;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;

namespace DevFramework.Core.DataAccess.NHibernate
{
    public class NhEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private NHibernateHelper _nHibernateHelper;

        public NhEntityRepositoryBase(NHibernateHelper nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }
        public TEntity Add(TEntity entity)
        {
            using (ISession _session = NHibernateHelper.OpenSesion())
            {
                _session.Save(entity);
                return entity;
            }
        }

        public void Delete(TEntity entity)
        {
            using (var Isession = NHibernateHelper.OpenSesion())
            {
                using (ITransaction _transaction = Isession.BeginTransaction())
                {
                    try
                    {
                        Isession.Delete(entity);
                        _transaction.Commit();
                    }
                    catch (System.Exception ex)
                    {
                        if (!_transaction.WasCommitted)
                            _transaction.Rollback();
                        throw new System.Exception("Delete hata :" + ex.Message);
                    }
                }
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var Isession = NHibernateHelper.OpenSesion())
            {
                return Isession.Get<TEntity>(filter);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var Isession = NHibernateHelper.OpenSesion())
            {
                return filter == null ?
                    Isession.Query<TEntity>().ToList()
                    : Isession.Query<TEntity>().Where(filter).ToList();
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (var Isession = NHibernateHelper.OpenSesion())
            {
                using (ITransaction _transaction = Isession.BeginTransaction())
                {
                    try
                    {
                        Isession.Update(entity);
                        _transaction.Commit();
                    }
                    catch (System.Exception ex)
                    {
                        if (!_transaction.WasCommitted)
                            _transaction.Rollback();
                        throw new System.Exception("Update hata :" + ex.Message);
                    }
                }
            }
            return entity;
        }
    }
}
