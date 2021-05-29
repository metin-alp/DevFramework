using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFramework.Core.DataAccess.NHibernate;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace DevFramework.Northwind.DataAccess.Concrete.NHibernate.Helpers
{
    public class SqlServerHelper : NHibernateHelper
    {

        private static ISessionFactory _sesisonFactories;

        public IPersistenceConfigurer GetConfiguration(Database selectedDatabase)
        {
            var configuration = MsSqlConfiguration.MsSql2012
                .ConnectionString(@"Data Source =(localdb)\MSSQLLocalDB; Initial Catalog = NorthWind; Integrated Security = True; MultipleActiveResultSets = true")
                .ShowSql();
            return configuration;
        }


        /*public ISessionFactory InitializeFactory()
        {
            // var cfg = new Fluently();
            // cfg.BeginConfigure(); // read config default style
            //return Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(y=> y.Server("NorthwindContext")))
            //     .Mappings(
            //       m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
            //     .BuildSessionFactory();
            // var config = MsSqlConfiguration.MsSql2012.ConnectionString(t=>t.fr)


            // var connString = @"Data Source =(localdb)\MSSQLLocalDB; Initial Catalog = NorthWind; Integrated Security = True; MultipleActiveResultSets = true";
            //var FnhDbString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = NorthWind; Integrated Security = True";


            /* return Fluently.Configure()
             .Database(
                 MsSqlConfiguration.MsSql2012
                     .ConnectionString(
                         cs => cs.FromConnectionStringWithKey
                               ("DBConnection")))
             .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
             .BuildSessionFactory();*/


        //return Fluently.Configure().Database(MsSqlConfiguration.MsSql2012
        //    .ConnectionString(c=>c.FromConnectionStringWithKey("NorthwindContext")))
        //    //.Mappings(t => t.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())).BuildSessionFactory();
        //    .Mappings(t => t.FluentMappings.AddFromAssemblyOf<Product>()).BuildSessionFactory();*/

    }
}
