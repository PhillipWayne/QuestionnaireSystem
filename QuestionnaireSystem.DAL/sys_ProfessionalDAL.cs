using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using QuestionnaireSystem.DBUility;
using QuestionnaireSystem.Model;
namespace QuestionnaireSystem.DAL
{
    public   class sys_ProfessionalDAL
    {
        #region 添加专业
        /// <summary>
        /// 根据专业实体对象添加专业信息，1 成功 0 失败 -1 出错
        /// </summary>
        /// <param name="professModel">专业实体对象</param>
        /// <returns></returns>
        public int AddProfessional(sys_ProfessionalModel professModel)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into sys_Professional (ProfessionalName,ProfessionalState ) values ( ");
                strSql.Append("@ProfessionalName,@ProfessionalState ) ");
                SqlParameter[] paras ={
                                      new SqlParameter("@ProfessionalName",SqlDbType.VarChar,50),
                                      new SqlParameter("@ProfessionalState",SqlDbType.Int,4)
                                  };
                paras[0].Value = professModel.ProfessionalName;
                paras[1].Value = professModel.ProfessionalState;
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

        #region 修改专业
        /// <summary>
        /// 根据专业实体对象修改专业信息 1成功 0 失败 -1出错
        /// </summary>
        /// <param name="professModel">专业实体对象</param>
        /// <returns></returns>
        public int UpdateProfessional(sys_ProfessionalModel professModel)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update sys_Professional set ProfessionalName=@ProfessionalName, ");
                strSql.Append(" ProfessionalState=@ProfessionalState ");
                strSql.Append(" where ProfessionalId =@ProfessionalId ");
                SqlParameter[] paras ={
                                      new SqlParameter("@ProfessionalName",SqlDbType.VarChar,50),
                                      new SqlParameter("@ProfessionalState",SqlDbType.Int,4),
                                      new SqlParameter("@ProfessionalId",SqlDbType.Int,4)
                                  };
                paras[0].Value = professModel.ProfessionalName;
                paras[1].Value = professModel.ProfessionalState;
                paras[2].Value = professModel.ProfessionalId;
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
        /// 根据专业编号进行状态停用启用切换 1成功 0 失败 -1 出错
        /// </summary>
        /// <param name="ProfessionalId">专业编号</param>
        /// <returns></returns>
        public int UpdateProfessionalStateByProfessId(int ProfessionalId)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update sys_Professional set ProfessionalState=3-ProfessionalState ");
                strSql.Append(" where ProfessionalId=@ProfessionalId");
                SqlParameter[] paras = {
                                       new SqlParameter("@ProfessionalId",SqlDbType.Int,4)
                                   };
                paras[0].Value = ProfessionalId;
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

        #region 查询专业
        /// <summary>
        /// 根据专业编号获取该专业信息 
        /// </summary>
        /// <param name="ProfessionalId">专业编号</param>
        /// <returns></returns>
        public sys_ProfessionalModel GetProfessionalByProfessId(int ProfessionalId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ProfessionalId,ProfessionalName,ProfessionalState from sys_Professional");
            strSql.Append(" where ProfessionalId=@ProfessionalId ");
            SqlParameter[] paras ={
                                      new SqlParameter("@ProfessionalId",SqlDbType.Int,4)
                                 };
            paras[0].Value = ProfessionalId;
            sys_ProfessionalModel professModel = new sys_ProfessionalModel();
            SqlDataReader reader = DBHelper.ExecuteReader(strSql.ToString(), paras);
            if (reader != null)
            {
                while (reader.Read())
                {
                    professModel.ProfessionalId = reader.GetInt32(0);
                    professModel.ProfessionalName = reader.GetString(1);
                    professModel.ProfessionalState = reader.GetInt32(2);
                }
            }
            reader.Close();
            return professModel;
        }
        /// <summary>
        /// 根据查询条件获取专业信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public DataTable GetProfessionalByWhere(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ProfessionalId,ProfessionalName,ProfessionalState from sys_Professional ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where "+strWhere);
            }
            DataSet ds = DBHelper.Query(strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 使用分页存储过程，分页查询专业信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="pageNo">当前页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordTotal">总记录数</param>
        /// <returns></returns>
        public DataTable GetProfessionalByProc(string strWhere,int pageNo,int pageSize,out int recordTotal)
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
            parameters[1].Value = " sys_Professional ";
            parameters[2].Value = "ProfessionalId,ProfessionalName,ProfessionalState";
            parameters[3].Value = "ProfessionalId";
            parameters[4].Value = pageSize;
            parameters[5].Value = pageNo;
            parameters[6].Value = "ProfessionalState asc,ProfessionalId asc";
            parameters[7].Value = strWhere;
            DataTable dt = DBHelper.RunProcedure(procName, parameters, out recordTotal);
            return dt;
        }
        #endregion
    }
}
