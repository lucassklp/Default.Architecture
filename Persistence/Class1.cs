//using MySql.Data.MySqlClient;
//using DefaultArchitecture.Persistence.Map;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Core.Objects;
//using System.Data.Entity.Infrastructure;
//using System.Data.Entity.ModelConfiguration.Conventions;
//using System.Data.SqlClient;
//using System.Diagnostics;
//using System.Linq;

//namespace DefaultArchitecture.Persistence
//{
//    public class DaoContext : DbContext
//    {
//        private static DaoContext instance;
//        private ObjectContext objectContext;

//        private DaoContext()
//            : base("DefaultArchitectureEntities")
//        {
//            Setup();
//        }

//        private void Setup()
//        {
//            Configure();

//            objectContext = ((IObjectContextAdapter)this).ObjectContext;
//#if DEBUG
//            Debug.WriteLine(string.Format("Script: {0}", objectContext.CreateDatabaseScript()));
//            Database.Log = data => Debug.WriteLine(string.Format("DaoContext: {0}", data));
//#endif
//        }

//        private void Configure()
//        {
//            Configuration.LazyLoadingEnabled = true;
//            Configuration.ProxyCreationEnabled = true;
//        }

//        public static DaoContext GetInstance()
//        {
//            if (instance == null)
//                instance = new DaoContext();
//            return instance;
//        }

//        /// <summary>
//        /// Manipulate a Table using Entity Framework
//        /// </summary>
//        /// <typeparam name="T">Entity to Manipulate</typeparam>
//        /// <returns>A DbSet Object to manipulate the database.</returns>
//        public DbSet<T> Manipulate<T>() where T : class
//        {
//            return base.Set<T>();
//        }

//        /// <summary>
//        /// Call a Stored Procedure
//        /// </summary>
//        /// <typeparam name="T">Return type of Stored Procedure. 
//        /// Fields on query must have the name as property of this type.</typeparam>
//        /// <param name="procedureName">Name of procedure you are calling</param>
//        /// <returns>A list of result os Stored Procedure</returns>
//        public IList<T> CallStoredProcedure<T>(string procedureName)
//        {
//            return this.Database.SqlQuery<T>($"CALL {procedureName}").ToList();
//        }

//        /// <summary>
//        /// Call a Stored Procedure
//        /// </summary>
//        /// <typeparam name="T">Return type of Stored Procedure.
//        /// Fields on query must have the name as property of this type.</typeparam>
//        /// <param name="procedureName">Name of procedure you are calling</param>
//        /// <param name="parameters">Parameters of Stored Procedure</param>
//        /// <returns>A list of result os Stored Procedure</returns>
//        public IList<T> CallStoredProcedure<T>(string procedureName, params MySqlParameter[] parameters)
//        {
//            string sqlProcedureParameters = string.Empty;
//            foreach (var item in parameters)
//                sqlProcedureParameters += $"@{item.ParameterName}, ";

//            sqlProcedureParameters = sqlProcedureParameters.Remove(sqlProcedureParameters.LastIndexOf(", "), 2);

//            return this.Database.SqlQuery<T>($"CALL {procedureName}({sqlProcedureParameters})", parameters).ToList();
//        }

//        /// <summary>
//        /// Call a Stored Procedure
//        /// </summary>
//        /// <typeparam name="T">Return type of Stored Procedure.
//        /// Fields on query must have the name as property of this type.</typeparam>
//        /// <param name="procedureName">Name of procedure you are calling</param>
//        public void CallStoredProcedure(string procedureName)
//        {
//            this.Database.ExecuteSqlCommand($"CALL {procedureName}");
//        }


//        /// <summary>
//        /// Call a Stored Procedure
//        /// </summary>
//        /// <typeparam name="T">Return type of Stored Procedure.
//        /// Fields on query must have the name as property of this type.</typeparam>
//        /// <param name="procedureName">Name of procedure you are calling</param>
//        /// <param name="parameters">Parameters of Stored Procedure</param>
//        public void CallStoredProcedure(string procedureName, params MySqlParameter[] parameters)
//        {
//            string sqlProcedureParameters = string.Empty;
//            foreach (var item in parameters)
//                sqlProcedureParameters += $"@{item.ParameterName}, ";

//            sqlProcedureParameters = sqlProcedureParameters.Remove(sqlProcedureParameters.LastIndexOf(", "), 2);

//            this.Database.ExecuteSqlCommand($"CALL {procedureName}({sqlProcedureParameters})", parameters);
//        }


//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.Configurations.Add(new AddressMap());
//            modelBuilder.Configurations.Add(new ClientMap());
//            modelBuilder.Configurations.Add(new EmployeeMap());
//            modelBuilder.Configurations.Add(new PersonMap());
//            modelBuilder.Configurations.Add(new ProductMap());
//            modelBuilder.Configurations.Add(new ProviderMap());
//            modelBuilder.Configurations.Add(new SaleItemMap());
//            modelBuilder.Configurations.Add(new SaleMap());
//            modelBuilder.Configurations.Add(new PriceHistoryMap());

//            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
//            base.OnModelCreating(modelBuilder);
//        }

//    }
//}