using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
namespace DPDModelDPDModel.DAL
{
    public class SQLHelper
    {
        #region "SQL Sever"
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        //private static string ConnStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        private static string ConnStr = "server=47.88.149.87;database=db_SFI;uid=sfiuser;pwd=skyfaith";


        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <param name="conn">数据库连接</param>
        private static void OpenConnection(IDbConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="conn">数据库连接</param>
        private static void CloseConnection(IDbConnection conn)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 获取数据库的第一行第一列的值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public static string GetStringFromDB(string sql)
        {
            using (IDbConnection conn = new SqlConnection(ConnStr))
            {
                OpenConnection(conn);
                return (string)conn.ExecuteScalar(sql);
            }
        }
        /// <summary>
        /// 获取指定类型的list数据
        /// </summary>
        /// <typeparam name="T">查询数据对应的model类型</typeparam>
        /// <param name="sqlStr">sql语句</param>
        /// <returns></returns>
        public static IEnumerable<T> GetObject<T>(string sqlStr)
        {

            using (IDbConnection conn = new SqlConnection(ConnStr))
            {
                OpenConnection(conn);
                return conn.Query<T>(sqlStr);
            }

        }
        /// <summary>
        /// 获取指定类型的list数据
        /// </summary>
        /// <typeparam name="T">查询数据对应的model类型</typeparam>
        /// <param name="procName">存储过程的名称</param>
        /// <param name="paras">存储过程所需要的参数</param>
        /// <returns></returns>
        public static IEnumerable<T> GetObjectByProc<T>(string procName, object paras)
        {

            using (IDbConnection conn = new SqlConnection(ConnStr))
            {
                OpenConnection(conn);
                return conn.Query<T>(procName, paras, null, false, null, CommandType.StoredProcedure);
            }

        }

        /// <summary>
        /// 插入或更新数据库
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <returns></returns>
        public static bool UpDateSQL(string sqlStr)
        {
            using (IDbConnection conn = new SqlConnection(ConnStr))
            {
                OpenConnection(conn);
                int result = conn.Execute(sqlStr, commandTimeout: 300);
                CloseConnection(conn);
                return result == 0 ? false : true;
            }
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="procName">存储过程的名称</param>
        /// <param name="paras">存储过程需要的参数</param>
        /// <param name="returnParaName">存储过程返回值参数的名称</param>
        /// <returns></returns>
        public static T ExecuteStoredProcedure<T>(string procName, DynamicParameters paras, string returnParaName)
        {
            using (IDbConnection conn = new SqlConnection(ConnStr))
            {
                OpenConnection(conn);
                conn.Execute(procName, paras, null, null, CommandType.StoredProcedure);
                return paras.Get<T>(returnParaName);
            }
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="paras">存储过程参数</param>
        /// <returns></returns>
        public static bool ExecuteStoredProcedure(string procName, object paras)
        {
            using (IDbConnection conn = new SqlConnection(ConnStr))
            {
                OpenConnection(conn);
                return conn.Execute(procName, paras, null, null, CommandType.StoredProcedure) > 0 ? true : false;
            }
        }

        public static DataSet GetDataSet(string sql)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                IDataAdapter adater = new SqlDataAdapter(sql,conn);
                DataSet ds = new DataSet();
                adater.Fill(ds);
                return ds;
            }
        }
        #endregion
    }
}
