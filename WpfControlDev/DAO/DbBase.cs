//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Common;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace WpfControlDev.DAO
//{
//    public class DbBase : IDisposable
//    {
//        private string paramPrefix = "@";
//        private string providerName = "System.Data.SQLite";
//        private IDbConnection dbConnecttion;
//        private DbProviderFactory dbFactory;
//        private DBType _dbType = DBType.SqlServer;
//        public IDbConnection DbConnecttion
//        {
//            get
//            {
//                return dbConnecttion;
//            }
//        }
//        public IDbTransaction DbTransaction
//        {
//            get
//            {
//                return dbConnecttion.BeginTransaction();
//            }
//        }
//        public string ParamPrefix
//        {
//            get
//            {
//                return paramPrefix;
//            }
//        }
//        public string ProviderName
//        {
//            get
//            {
//                return providerName;
//            }
//        }
//        public DBType DbType
//        {
//            get
//            {
//                return _dbType;
//            }
//        }

//        public DbBase(string connectionStringName)
//        {
//            var connStr = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
//            if (!string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName))
//                providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;
//            else
//                throw new Exception("ConnectionStrings中没有配置提供程序ProviderName！");
//            dbFactory = DbProviderFactories.GetFactory(providerName);
//            dbConnecttion = dbFactory.CreateConnection();
//            dbConnecttion.ConnectionString = connStr;
//            dbConnecttion.Open();
//            SetParamPrefix();
//        }


//        private void SetParamPrefix()
//        {
//            string dbtype = (dbFactory == null ? dbConnecttion.GetType() : dbFactory.GetType()).Name;

//            // 使用类型名判断
//            if (dbtype.StartsWith("MySql")) _dbType = DBType.MySql;
//            else if (dbtype.StartsWith("SqlCe")) _dbType = DBType.SqlServerCE;
//            else if (dbtype.StartsWith("Npgsql")) _dbType = DBType.PostgreSQL;
//            else if (dbtype.StartsWith("Oracle")) _dbType = DBType.Oracle;
//            else if (dbtype.StartsWith("SQLite")) _dbType = DBType.SQLite;
//            else if (dbtype.StartsWith("System.Data.SqlClient.")) _dbType = DBType.SqlServer;
//            // else try with provider name
//            else if (providerName.IndexOf("MySql", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.MySql;
//            else if (providerName.IndexOf("SqlServerCe", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.SqlServerCE;
//            else if (providerName.IndexOf("Npgsql", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.PostgreSQL;
//            else if (providerName.IndexOf("Oracle", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.Oracle;
//            else if (providerName.IndexOf("SQLite", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.SQLite;

//            if (_dbType == DBType.MySql && dbConnecttion != null && dbConnecttion.ConnectionString != null && dbConnecttion.ConnectionString.IndexOf("Allow User Variables=true") >= 0)
//                paramPrefix = "?";
//            if (_dbType == DBType.Oracle)
//                paramPrefix = ":";
//        }

//        public void Dispose()
//        {
//            if (dbConnecttion != null)
//            {
//                try
//                {
//                    dbConnecttion.Dispose();
//                }
//                catch { }
//            }
//        }
//    }
//    public enum DBType
//    {
//        SqlServer,
//        SqlServerCE,
//        MySql,
//        PostgreSQL,
//        Oracle,
//        SQLite
//    }

//    public class DBHelper
//    {
//        /// <summary>
//        /// 插入数据
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="dbs"></param>
//        /// <param name="t"></param>
//        /// <param name="transaction"></param>
//        /// <param name="commandTimeout"></param>
//        /// <returns></returns>
//        public static bool Insert<T>(this DbBase dbs, T t, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
//        {
//            var db = dbs.DbConnecttion;
//            var sql = SqlQuery<T>.Builder(dbs);
//            var flag = db.Execute(sql.InsertSql, t, transaction, commandTimeout);
//            return flag == 1;
//        }

//        /// <summary>
//        /// 插入数据
//        /// </summary>
//        /// <typeparam name="T">模型类型</typeparam>
//        /// <param name="dbs">理解数据</param>
//        /// <param name="t">数据内容</param>
//        /// <param name="transaction">事物</param>
//        /// <param name="commandTimeout">连接时间</param>
//        /// <returns></returns>
//        public static int InsertTwo<T>(this DbBase dbs, T t, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
//        {
//            var db = dbs.DbConnecttion;
//            var sql = SqlQuery<T>.Builder(dbs);
//            NewId identity = new NewId();

//            string strSql = sql.InsertSql + ";SELECT LAST_INSERT_ID() as Id;";
//            identity = db.Query<NewId>(strSql, t, transaction, true, commandTimeout, null).Single<NewId>();

//            return identity.Id;
//        }

//        /// <summary>
//        ///  批量插入数据
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="dbs"></param>
//        /// <param name="lt"></param>
//        /// <param name="transaction"></param>
//        /// <param name="commandTimeout"></param>
//        /// <returns></returns>
//        public static bool InsertBatch<T>(this DbBase dbs, IList<T> lt, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
//        {
//            var db = dbs.DbConnecttion;
//            var sql = SqlQuery<T>.Builder(dbs);
//            var flag = db.Execute(sql.InsertSql, lt, transaction, commandTimeout);
//            return flag == lt.Count;
//        }

//        /// <summary>
//        /// 按条件删除
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="dbs"></param>
//        /// <param name="sql"></param>
//        /// <returns></returns>
//        public static bool Delete<T>(this DbBase dbs, SqlQuery sql = null) where T : class
//        {
//            var db = dbs.DbConnecttion;
//            if (sql == null)
//            {
//                sql = SqlQuery<T>.Builder(dbs);
//            }
//            var f = db.Execute(sql.DeleteSql, sql.Param);
//            return f > 0;
//        }

//        /// <summary>
//        /// 修改
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="dbs"></param>
//        /// <param name="t">如果sql为null，则根据t的主键进行修改</param>
//        /// <param name="sql">按条件修改</param>
//        /// <returns></returns>
//        public static bool Update<T>(this DbBase dbs, T t, SqlQuery sql = null) where T : class
//        {
//            var db = dbs.DbConnecttion;
//            if (sql == null)
//            {
//                sql = SqlQuery<T>.Builder(dbs);
//            }
//            sql = sql.AppendParam<T>(t);
//            var f = db.Execute(sql.UpdateSql, sql.Param);
//            return f > 0;
//        }
//        /// <summary>
//        /// 修改
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="dbs"></param>
//        /// <param name="t">如果sql为null，则根据t的主键进行修改</param>
//        /// <param name="updateProperties">要修改的属性集合</param>
//        /// <param name="sql">按条件修改</param>
//        /// <returns></returns>
//        public static bool Update<T>(this DbBase dbs, T t, IList<string> updateProperties, SqlQuery sql = null) where T : class
//        {
//            var db = dbs.DbConnecttion;
//            if (sql == null)
//            {
//                sql = SqlQuery<T>.Builder(dbs);
//            }
//            sql = sql.AppendParam<T>(t)
//                .SetExcProperties<T>(updateProperties);
//            var f = db.Execute(sql.UpdateSql, sql.Param);
//            return f > 0;
//        }

//        /// <summary>
//        /// 修改
//        /// </summary>
//        /// <typeparam name="T">模型类型</typeparam>
//        /// <param name="dbs">db</param>
//        /// <param name="t">修改的数据</param>
//        /// <param name="sql">修改的SQL语句</param>
//        /// <returns></returns>
//        public static bool Update<T>(this DbBase dbs, T t, string sql) where T : class
//        {
//            var db = dbs.DbConnecttion;
//            var f = db.Execute(sql, t);
//            return f > 0;
//        }
//    }
//}
