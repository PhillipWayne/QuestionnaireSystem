using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
//引用命名空间
using System.Data.SqlClient;
using System.Data;

namespace QuestionnaireSystem.DBUility
{
    public static class DBHelper
    {
        //public static string connString = ConfigurationManager.AppSettings[0].ToString();
        public static string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        #region 执行简单的SQL语句
        /// <summary>
        /// 执行一条SQL语句，得到一个结果值
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string strSql)
        {
            //using 会自动释放其内部使用变量的内存
            //using(表达式) 表达式不是起判断作用，而是首先执行的意思
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(strSql, conn))
                {
                    try
                    {
                        conn.Open();
                        object obj = cmd.ExecuteScalar();
                        return obj;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 操作SQL语句，得到对数据库的影响行数 如果大于0执行成功 等于0执行失败 小于0执行出错
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string strSql)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(strSql, conn))
                {
                    try
                    {
                        conn.Open();
                        int number = cmd.ExecuteNonQuery();
                        return number;
                    }
                    catch (Exception ex)
                    {
                        return -1;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 操作SQL语句，得到一个数据读取器SqlDataReader
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string strSql)
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dataReader.HasRows)
                {
                    return dataReader;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return null;
               
            }
            
        }
        
        /// <summary>
        ///  操作SQL语句，得到数据集
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataSet Query(string strSql)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(strSql, conn);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        #endregion
        #region 带参数的SQL语句操作
        /// <summary>
        /// 执行带有参数的SQL语句，得到一个结果值
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="Parameters">SQL语句参数值数组</param>
        /// <returns></returns>
        public static object ExecuteScalar(string strSql,params SqlParameter[] Parameters)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(strSql, conn))
                {
                    try
                    {
                        conn.Open();
                        foreach (SqlParameter par in Parameters)
                        {
                            cmd.Parameters.Add(par);
                        }
                        object obj = cmd.ExecuteScalar();
                        return obj;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 操作带参数SQL语句，得到对数据库的影响行数 如果大于0执行成功 等于0执行失败 小于0执行出错
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="Parameters">参数数组</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string strSql,params SqlParameter[] Parameters)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(strSql, conn))
                {
                    try
                    {
                        conn.Open();
                        foreach (SqlParameter para in Parameters)
                        {
                            cmd.Parameters.Add(para);
                        }
                        int number = cmd.ExecuteNonQuery();
                        return number;
                    }
                    catch (Exception ex)
                    {
                        return -1;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 操作带参数的SQL语句，得到一个数据读取器SqlDataReader
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="Parameters">参数数组</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string strSql,params SqlParameter[] Parameters)
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                foreach(SqlParameter para in Parameters){
                    cmd.Parameters.Add(para);
                }
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dataReader.HasRows)
                {
                    return dataReader;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return null;

            }

        }

        /// <summary>
        ///  操作SQL语句，得到数据集
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataSet Query(string strSql,params SqlParameter[] Parameters)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(strSql, conn);
                    if (Parameters!=null)
                    {
                        foreach (SqlParameter para in Parameters)
                        {
                            cmd.Parameters.Add(para);
                        } 
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        #endregion

        #region 带有事务操作
        public static int ExecuteNonQueryByTran(string strSql, params SqlParameter[] Parameters)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand(strSql, conn,tran))
                    {
                        try
                        {
                            conn.Open();
                            foreach (SqlParameter para in Parameters)
                            {
                                cmd.Parameters.Add(para);
                            }
                            int number = cmd.ExecuteNonQuery();
                            tran.Commit();
                            return number;
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            return -1;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }
        public static int ExecuteSqlTran(List<CommandInfo> cmdList)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                if(conn.State!=ConnectionState.Open)
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int count = 0;
                        if (cmdList!=null)
                        {
                            foreach (CommandInfo myDE in cmdList)
                            {
                                cmd.CommandText = myDE.CommandText;
                                cmd.Connection = conn;
                                cmd.Transaction = tran;
                                SqlParameter[] paras = (SqlParameter[])myDE.Parameters;
                                if (paras!=null)
                                {
                                    foreach (SqlParameter para in paras)
                                    {
                                        cmd.Parameters.Add(para);
                                    } 
                                }
                                if (myDE.EffentNextType == EffentNextType.WhenHaveContine || myDE.EffentNextType == EffentNextType.WhenNoHaveContine)
                                {
                                    if (myDE.CommandText.ToLower().IndexOf("count(") == -1)
                                    {
                                        tran.Rollback();
                                        return 0;
                                    }
                                    object obj = cmd.ExecuteScalar();
                                    bool isHave = false;
                                    if (obj == null && obj == DBNull.Value)
                                    {
                                        isHave = false;
                                    }
                                    if (myDE.EffentNextType == EffentNextType.WhenHaveContine && !isHave)
                                    {
                                        tran.Rollback();
                                        return 0;
                                    }
                                    if (myDE.EffentNextType == EffentNextType.WhenNoHaveContine && isHave)
                                    {
                                        tran.Rollback();
                                        return 0;
                                    }
                                    continue;
                                }
                                int val = cmd.ExecuteNonQuery();
                                count += val;
                                if (myDE.EffentNextType == EffentNextType.ExcuteEffectRows && val == 0)
                                {
                                    tran.Rollback();
                                    return 0;
                                }
                                cmd.Parameters.Clear();
                            } 
                        }
                        tran.Commit();
                        return count;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
            }
        }
        #endregion

        #region 执行存储过程的操作
        /// <summary>
        /// 执行分页存储过程，得到对应页面的记录信息
        /// </summary>
        /// <param name="storedProcName">存储过程</param>
        /// <param name="parameters">存储过程中参数集合</param>
        /// <param name="rowsAffected">可以返回值的总记录数</param>
        /// <returns></returns>
        public static DataTable RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using(SqlConnection conn = new SqlConnection(connString)){
                try
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcName, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (SqlParameter para in parameters)
                        {
                            cmd.Parameters.Add(para);
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        adapter.Dispose();
                        conn.Close();
                        conn.Dispose();
                        rowsAffected = (int)cmd.Parameters["@recordTotal"].Value;
                        return ds.Tables[0];
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        #endregion
    }
}
