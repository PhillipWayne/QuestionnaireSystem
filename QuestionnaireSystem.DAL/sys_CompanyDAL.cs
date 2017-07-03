using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuestionnaireSystem.Model;
using System.Data.SqlClient;
using System.Data;
using QuestionnaireSystem.DBUility;

namespace QuestionnaireSystem.DAL
{
    /// <summary>
    /// 所属企业数据访问类
    /// </summary>
    public   class sys_CompanyDAL
    {
        #region 添加企业
        /// <summary>
        /// 把企业实体类信息添加到数据库中，得到对数据库操作结果如果大于0添加成功，如果等于0添加失败，如果小于0添加出错
        /// </summary>
        /// <param name="companyModel">企业实体类信息</param>
        /// <returns></returns>
        public int AddCompany(sys_CompanyModel companyModel)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into sys_Company ( ");
                strSql.Append(" CompanyName,CompanyAddress,CompanyPerson,CompanyTelPhone, ");
                strSql.Append(" CompanyOthInfo,CompanyState ) values ( ");
                strSql.Append(" @CompanyName,@CompanyAddress,@CompanyPerson,@CompanyTelPhone,");
                strSql.Append(" @CompanyOthInfo,@CompanyState )");
                SqlParameter[] paras ={
                                      new SqlParameter("@CompanyName",SqlDbType.VarChar,50),
                                      new SqlParameter("@CompanyAddress",SqlDbType.VarChar,200),
                                      new SqlParameter("@CompanyPerson",SqlDbType.VarChar,50),
                                      new SqlParameter("@CompanyTelPhone",SqlDbType.VarChar,50),
                                      new SqlParameter("@CompanyOthInfo",SqlDbType.VarChar,500),
                                      new SqlParameter("@CompanyState",SqlDbType.Int,4)
                                  };
                paras[0].Value = companyModel.CompanyName;
                paras[1].Value = companyModel.CompanyAddress;
                paras[2].Value = companyModel.CompanyPerson;
                paras[3].Value = companyModel.CompanyTelPhone;
                paras[4].Value = companyModel.CompanyOthInfo;
                paras[5].Value = companyModel.CompanyState;
                int number = DBHelper.ExecuteNonQuery(strSql.ToString(), paras);
                return number;
            }
            catch (Exception)
            {
                return -1;
                throw ;
            }
        }
        #endregion
        #region 修改企业
        /// <summary>
        /// 根据企业实体信息，修改企业信息，得到对数据库操作结果 如果大于0操作成功，等于0操作失败，小于0操作出错
        /// </summary>
        /// <param name="companyModel">企业实体信息</param>
        /// <returns></returns>
        public int UpdateCompany(sys_CompanyModel companyModel)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update sys_Company set CompanyName =@CompanyName,");
                strSql.Append(" CompanyAddress=@CompanyAddress,CompanyPerson=@CompanyPerson,");
                strSql.Append(" CompanyTelPhone=@CompanyTelPhone,CompanyOthInfo=@CompanyOthInfo,");
                strSql.Append(" CompanyState=@CompanyState where CompanyId=@CompanyId ");
                SqlParameter[] paras ={
                                      new SqlParameter("@CompanyName",SqlDbType.VarChar,50),
                                      new SqlParameter("@CompanyAddress",SqlDbType.VarChar,200),
                                      new SqlParameter("@CompanyPerson",SqlDbType.VarChar,50),
                                      new SqlParameter("@CompanyTelPhone",SqlDbType.VarChar,50),
                                      new SqlParameter("@CompanyOthInfo",SqlDbType.VarChar,500),
                                      new SqlParameter("@CompanyState",SqlDbType.Int,4),
                                      new SqlParameter("@CompanyId",SqlDbType.Int,4)

                                  };
                paras[0].Value = companyModel.CompanyName;
                paras[1].Value = companyModel.CompanyAddress;
                paras[2].Value = companyModel.CompanyPerson;
                paras[3].Value = companyModel.CompanyTelPhone;
                paras[4].Value = companyModel.CompanyOthInfo;
                paras[5].Value = companyModel.CompanyState;
                paras[6].Value = companyModel.CompanyId;
                int number = DBHelper.ExecuteNonQuery(strSql.ToString(), paras);
                return number;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }
        /// <summary>
        /// 根据企业编号修改用户状态启用停用相互切换
        /// </summary>
        /// <param name="CompanyId">企业编号</param>
        /// <returns></returns>
        public int UpdateCompanyStateByCompanyId(int CompanyId)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update sys_Company set CompanyState=3-CompanyState where CompanyId=@CompanyId ");
                SqlParameter[] paras ={
                                      new SqlParameter("@CompanyId",SqlDbType.Int,4)
                                  };
                paras[0].Value = CompanyId;
                int number = DBHelper.ExecuteNonQuery(strSql.ToString(), paras);
                return number;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }
        #endregion
        #region 查询企业
        /// <summary>
        /// 根据企业编号查询该企业信息，得到企业实体类信息
        /// </summary>
        /// <param name="CompanyId">企业编号</param>
        /// <returns></returns>
        public sys_CompanyModel GetCompanyByCompanyId(int CompanyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 CompanyId,CompanyName,CompanyAddress,CompanyPerson,");
            strSql.Append("CompanyTelPhone,CompanyOthInfo,CompanyState from sys_Company ");
            strSql.Append(" where CompanyId=@CompanyId ");
            SqlParameter[] paras ={
                                      new SqlParameter("@CompanyId",SqlDbType.Int,4)
                                  };
            paras[0].Value = CompanyId;
            sys_CompanyModel companyModel = new sys_CompanyModel();
            SqlDataReader reader = DBHelper.ExecuteReader(strSql.ToString(), paras);
            if (reader != null)
            {
                while (reader.Read())
                {
                    companyModel.CompanyId = reader.GetInt32(0);
                    companyModel.CompanyName = reader.GetString(1);
                    try
                    {
                        companyModel.CompanyAddress = reader.GetString(2);
                    }
                    catch (Exception)
                    {
                        companyModel.CompanyAddress = null;
                    }
                    try
                    {
                        companyModel.CompanyPerson = reader.GetString(3);
                    }
                    catch (Exception)
                    {
                        companyModel.CompanyPerson = null;
                    }
                    try
                    {
                        companyModel.CompanyTelPhone = reader.GetString(4);
                    }
                    catch (Exception)
                    {
                        companyModel.CompanyTelPhone = null;
                    }
                    try
                    {
                        companyModel.CompanyOthInfo = reader.GetString(5);
                    }
                    catch (Exception)
                    {
                        companyModel.CompanyOthInfo = null;
                    }
                    companyModel.CompanyState = reader.GetInt32(6);
                }
            }
            reader.Close();
            return companyModel;
        }
        /// <summary>
        /// 根据查询条件，查询企业信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public DataTable GetCompanyByWhere(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CompanyId,CompanyName,CompanyAddress,CompanyPerson,CompanyTelPhone,");
            strSql.Append("CompanyOthInfo,CompanyState from sys_Company ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where "+strWhere);
            }
            DataSet ds = DBHelper.Query(strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 使用分页存储过程，分页查询企业信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="pageNo">查询页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordTotal">总记录数</param>
        /// <returns></returns>
        public DataTable GetCompanyByProc(string strWhere, int pageNo, int pageSize, out int recordTotal)
        {
            recordTotal = 0;
            string procName = "ProcViewPager";
            SqlParameter[] parameters ={
                                          new SqlParameter("@recordTotal",SqlDbType.Int,4),
                                          new SqlParameter("@viewName",SqlDbType.VarChar,800),
                                          new SqlParameter("@fieldName",SqlDbType.VarChar,800),
                                          new SqlParameter("@keyName",SqlDbType.VarChar,200),
                                          new SqlParameter("@pageSize",SqlDbType.Int,4),
                                          new SqlParameter("@pageNo",SqlDbType.Int,4),
                                          new SqlParameter("@orderString",SqlDbType.VarChar,200),
                                          new SqlParameter("@whereString",SqlDbType.VarChar,800)
                                      };
            parameters[0].Value = recordTotal;
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = " sys_Company ";
            parameters[2].Value = "CompanyId,CompanyName,CompanyAddress,CompanyPerson,CompanyTelPhone,CompanyOthInfo,CompanyState";
            parameters[3].Value = "CompanyId";
            parameters[4].Value = pageSize;
            parameters[5].Value = pageNo;
            parameters[6].Value = "CompanyId asc,CompanyState asc";
            parameters[7].Value = strWhere;
            DataTable dt = DBHelper.RunProcedure(procName, parameters, out recordTotal);
            return dt;
        }
        #endregion
    }
}
