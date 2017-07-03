using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using QuestionnaireSystem.DBUility;
using QuestionnaireSystem.Model;

namespace QuestionnaireSystem.DAL
{
    public class sys_UsersDAL
    {
        #region 添加用户
        /// <summary>
        /// 将用户对象添加到数据库中，得到对数据库的影响行数，大于0表示插入成功 0表示插入失败 小于0表示插入出错
        /// </summary>
        /// <param name="userModel">用户实体对象</param>
        /// <returns></returns>
        public int InsertUser(sys_UsersModel userModel)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into sys_Users (UserName,UserPassword,RealName,");
                strSql.Append("SubordinateEnterprise,UserType,UserState)");
                strSql.Append(" values (@UserName,@UserPassword,@RealName,");
                strSql.Append("@SubordinateEnterprise,@UserType,@UserState)");
                SqlParameter[] paras ={
                                     new SqlParameter("@UserName",SqlDbType.VarChar,20),
                                     new SqlParameter("@UserPassword",SqlDbType.VarChar,20),
                                     new SqlParameter("@RealName",SqlDbType.VarChar,10),
                                     new SqlParameter("@SubordinateEnterprise",SqlDbType.Int,4),
                                     new SqlParameter("@UserType",SqlDbType.Int,4),
                                     new SqlParameter("@UserState",SqlDbType.Int)
                                 };
                paras[0].Value = userModel.UserName;
                paras[1].Value = userModel.UserPassword;
                paras[2].Value = userModel.RealName;
                paras[3].Value = userModel.SubordinateEnterprise;
                paras[4].Value = userModel.UserType;
                paras[5].Value = userModel.UserState;
                int number = DBHelper.ExecuteNonQuery(strSql.ToString(), paras);
                return number;
            }
            catch (Exception ex)
            {
                return -1;
                throw ex;
            }
        }
        #endregion
        #region 修改用户
        /// <summary>
        /// 根据用户对象，进行数据修改，得到对数据影响行数 大于0表示修改成功 等于0表示修改失败 小于0表示修改出错
        /// </summary>
        /// <param name="userModel">用户实体对象</param>
        /// <returns></returns>
        public int UpdateUser(sys_UsersModel userModel)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update sys_Users set ");
                strSql.Append("UserName=@UserName,");
                strSql.Append("UserPassword=@UserPassword,");
                strSql.Append("RealName=@RealName,");
                strSql.Append("SubordinateEnterprise=@SubordinateEnterprise,");
                strSql.Append("UserType=@UserType,");
                strSql.Append("UserState=@UserState");
                strSql.Append(" where UserId=@UserId");
                SqlParameter[] paras ={
                                      new SqlParameter("@UserName",SqlDbType.VarChar,20),
                                     new SqlParameter("@UserPassword",SqlDbType.VarChar,20),
                                     new SqlParameter("@RealName",SqlDbType.VarChar,10),
                                     new SqlParameter("@SubordinateEnterprise",SqlDbType.Int,4),
                                     new SqlParameter("@UserType",SqlDbType.Int,4),
                                     new SqlParameter("@UserState",SqlDbType.Int),
                                     new SqlParameter("@UserId",SqlDbType.Int)
                                  };
                paras[0].Value = userModel.UserName;
                paras[1].Value = userModel.UserPassword;
                paras[2].Value = userModel.RealName;
                paras[3].Value = userModel.SubordinateEnterprise;
                paras[4].Value = userModel.UserType;
                paras[5].Value = userModel.UserState;
                paras[6].Value = userModel.UserId;
                int numbers = DBHelper.ExecuteNonQuery(strSql.ToString(), paras);
                return numbers;
            }
            catch (Exception ex)
            {
                return -1;
                throw ex;
            }

        }

        /// <summary>
        /// 根据用户编号修改对应的密码 得到影响行数 如果大于0成功 等于0失败 小于0出错
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="password">新密码</param>
        /// <returns></returns>
        public int UpdatePasswordByUserId(int uid, string password)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update sys_Users set UserPassword=@UserPassword ");
                strSql.Append(" where UserId=@UserId");
                SqlParameter[] paras ={
                                     new SqlParameter("@UserPassword",SqlDbType.VarChar,20),
                                     new SqlParameter("@UserId",SqlDbType.Int)
                                 };
                paras[0].Value = password;
                paras[1].Value = uid;
                int numbers = DBHelper.ExecuteNonQuery(strSql.ToString(), paras);
                return numbers;
            }
            catch (Exception ex)
            {
                return -1;
                throw ex;
            }
        }

        #endregion
        #region 删除用户
        /// <summary>
        /// 根据用户编号，停用/启用该用户
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public int DeleteByUserId(int userId)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update sys_Users set UserState=3-UserState where UserId=@UserId");
                SqlParameter[] paras ={
                                      new SqlParameter("@UserId",SqlDbType.Int,4)
                                  };
                paras[0].Value = userId;
                int numbers = DBHelper.ExecuteNonQuery(strSql.ToString(), paras);
                return numbers;
            }
            catch (Exception ex)
            {
                return -1;
                throw ex;
            }
        }
        #endregion
        #region 查询用户
        /// <summary>
        /// 根据用户编号，查询出该用户信息，返回用户实体类对象
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public sys_UsersModel GetUsersModelByUserId(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserId,UserName,UserPassword,RealName,SubordinateEnterprise,CompanyName,");
            strSql.Append("UserType,UserState from sys_Users left join sys_Company on sys_Users.SubordinateEnterprise = sys_Company.CompanyId ");
            strSql.Append(" where UserId=@UserId");
            SqlParameter[] paras ={
                                     new SqlParameter("@UserId",SqlDbType.Int,4)
                                 };
            paras[0].Value = userId;
            SqlDataReader reader = DBHelper.ExecuteReader(strSql.ToString(), paras);
            sys_UsersModel userModel = new sys_UsersModel();
            if (reader != null)
            {
                while (reader.Read())
                {
                    userModel.UserId = reader.GetInt32(0);
                    userModel.UserName = reader.GetString(1);
                    userModel.UserPassword = reader.GetString(2);
                    userModel.RealName = reader.GetString(3);
                    try
                    {
                        userModel.SubordinateEnterprise = reader.GetInt32(4);
                    }
                    catch
                    {
                        userModel.SubordinateEnterprise = 0;
                    }
                    userModel.UserType = reader.GetInt32(6);
                    userModel.UserState = reader.GetInt32(7);
                }
            }
            else
            {
                return null;
            }
            reader.Close();
            return userModel;
        }

        /// <summary>
        /// 根据用户名查询该用户信息，返回用户实体类对象
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public sys_UsersModel GetUsersModelByUserName(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserId,UserName,UserPassword,RealName,SubordinateEnterprise,UserType,UserState from sys_Users ");
            strSql.Append(" where UserName=@UserName ");
            SqlParameter[] paras = {
                                           new SqlParameter("@UserName",SqlDbType.VarChar,20)
                                       };
            paras[0].Value = userName;
            SqlDataReader reader = DBHelper.ExecuteReader(strSql.ToString(), paras);
            sys_UsersModel userModel = new sys_UsersModel();
            if (reader != null)
            {
                while (reader.Read())
                {
                    userModel.UserId = reader.GetInt32(0);
                    userModel.UserName = reader.GetString(1);
                    userModel.UserPassword = reader.GetString(2);
                    userModel.RealName = reader.GetString(3);
                    userModel.SubordinateEnterprise = reader.GetInt32(4);
                    userModel.UserType = reader.GetInt32(5);
                    userModel.UserState = reader.GetInt32(6);
                }
            }
            else
            {
                return null;
            }
            reader.Close();
            return userModel;
        }

        public DataSet GetAllUser()
        {
            DataSet ds = DBHelper.Query("select * from sys_Users");
            return ds;
        }

        /// <summary>
        /// 根据条件查询用户信息，返回DataSet类型的数据集
        /// </summary>
        /// <param name="strWhere">查询条件，不需要加where关键字</param>
        /// <returns></returns>
        public DataSet GetUsersInfoByWhere(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserId,UserName,UserPassword,RealName,SubordinateEnterprise,");
            strSql.Append("CompanyName,UserType,UserState from sys_Users inner join sys_Company on ");
            strSql.Append(" sys_Users.SubordinateEnterprise =sys_Company.CompanyId ");
            if (strWhere != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by UserType asc,UserState asc ");
            DataSet ds = DBHelper.Query(strSql.ToString());
            return ds;
        }
        /// <summary>
        /// 根据存储过程查询用户信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="pageNo">当前的页码</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="recordTotal">总记录数</param>
        /// <returns></returns>
        public DataTable GetUsersInfoByProc(string strWhere,int pageNo,int pageSize,out int recordTotal)
        {
            recordTotal =0;
            string procName ="ProcViewPager";
            SqlParameter[] parameters={
                                          new SqlParameter("@recordTotal",SqlDbType.Int,4),
                                          new SqlParameter("@viewName",SqlDbType.VarChar,800),
                                          new SqlParameter("@fieldName",SqlDbType.VarChar,800),
                                          new SqlParameter("@keyName",SqlDbType.VarChar,200),
                                          new SqlParameter("@pageSize",SqlDbType.Int,4),
                                          new SqlParameter("@pageNo",SqlDbType.Int,4),
                                          new SqlParameter("@orderString",SqlDbType.VarChar,200),
                                          new SqlParameter("@whereString",SqlDbType.VarChar,800)
                                      };
            parameters[0].Value=recordTotal;
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value=" sys_Users inner join sys_Company on sys_Users.SubordinateEnterprise =sys_Company.CompanyId ";
            parameters[2].Value ="UserId,UserName,UserPassword,RealName,SubordinateEnterprise,CompanyName,UserType,UserState";
            parameters[3].Value="UserId";
            parameters[4].Value =pageSize;
            parameters[5].Value =pageNo;
            parameters[6].Value = "UserType asc,UserState asc";
            parameters[7].Value = strWhere;
            DataTable dt = DBHelper.RunProcedure(procName, parameters,out recordTotal);
            return dt;
        }
        #endregion
    }

}
