using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;


namespace DevFramework.Core.DataAccess.NHibernate
{
    public abstract class NHibernateHelper
    {
        private static ISessionFactory session;

        public static ISessionFactory CreateSession()
        {
            if (session != null)
                return session;

            FluentConfiguration _config = Fluently.Configure().Database(
             MsSqlConfiguration.MsSql2012.ConnectionString
             (x => x.Server(@"Data Source =(localdb)\MSSQLLocalDB;" +
                   " Initial Catalog=NorthWind;" +
                   " Integrated Security=True; MultipleActiveResultSets=true")
                   .Username("")
                   .Password("")
                   .Database("Northwind")))
               .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
               .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true));
            session = _config.BuildSessionFactory();
            return session;
        }
        public static ISession OpenSesion()
        {
            return CreateSession().OpenSession();
        }






        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
