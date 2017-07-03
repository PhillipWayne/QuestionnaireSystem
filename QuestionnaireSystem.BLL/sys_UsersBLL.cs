using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using QuestionnaireSystem.DAL;
using QuestionnaireSystem.Model;
using QuestionnaireSystem.Common;
namespace QuestionnaireSystem.BLL
{
    public class sys_UsersBLL
    {
        private readonly sys_UsersDAL sys_UserDAL = new sys_UsersDAL();
        #region  登录判断
        /// <summary>
        /// 根据用户名与密码，判断该用户是否能成功登录，返回提示信息
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        public string Login(string name, string password)
        {
            JsonMessage json = new JsonMessage();

            sys_UsersModel userModel = sys_UserDAL.GetUsersModelByUserName(name);
            if (userModel == null)
            {
                json.Success = false;
                json.Msg = "用户名不存在";
            }
            else
            {
                if (userModel.UserState == 1)
                {
                    if (string.Equals(userModel.UserPassword, password))
                    {
                        json.Success = true;
                        json.Msg = "登录成功";
                        StringBuilder str = new StringBuilder();
                        str.Append(userModel.UserId + ",");
                        str.Append(userModel.UserName + ",");
                        str.Append(userModel.SubordinateEnterprise + ",");
                        str.Append(userModel.UserType);
                        json.Obj = str.ToString();

                    }
                    else
                    {
                        json.Success = false;
                        json.Msg = "密码不正确";
                    }
                }
                else
                {
                    json.Success = false;
                    json.Msg = "该用户已被停用";
                }
            }
            return JsonHelper.ToJson(json);
        }
        #endregion

        #region 添加用户
        /// <summary>
        /// 添加用户信息到数据库中，得到返回结果
        /// </summary>
        /// <param name="userModel">用户实体对象</param>
        /// <returns></returns>
        public string AddUser(sys_UsersModel userModel)
        {
            JsonMessage json = new JsonMessage();
            if (sys_UserDAL.GetUsersModelByUserName(userModel.UserName) == null)
            {
                int number = sys_UserDAL.InsertUser(userModel);
                if (number > 0)
                {
                    json.Success = true;
                    json.Msg = "用户添加成功";
                }
                else if (number == 0)
                {
                    json.Success = false;
                    json.Msg = "用户添加失败";
                }
                else
                {
                    json.Success = false;
                    json.Msg = "用户添加出错";
                }
            }
            else
            {
                json.Success = false;
                json.Msg = "该用户名已存在";
            }
            return JsonHelper.ToJson(json);
        }
        #endregion

        #region 修改操作
        #region 修改密码
        /// <summary>
        /// 根据用户编号，原始密码，新密码进行修改用户密码，得到操作结果
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="mpass">用户原始密码</param>
        /// <param name="newpass">用户新密码</param>
        /// <returns></returns>
        public string UpdatePassword(string uid, string mpass, string newpass)
        {
            JsonMessage json = new JsonMessage();
            try
            {
                sys_UsersModel usersModel = sys_UserDAL.GetUsersModelByUserId(int.Parse(uid));
                if (usersModel != null)
                {
                    if (string.Equals(usersModel.UserPassword, mpass))
                    {
                        int number = sys_UserDAL.UpdatePasswordByUserId(int.Parse(uid), newpass);
                        if (number > 0)
                        {
                            json.Success = true;
                            json.Msg = "密码修改成功，请重新登录！";
                        }
                        else
                        {
                            json.Success = false;
                            json.Msg = "密码修改失败";
                        }
                    }
                    else
                    {
                        json.Success = false;
                        json.Msg = "原始密码不正确";
                    }
                }
                else
                {
                    json.Success = false;
                    json.Msg = "该用户不存在";
                }
            }
            catch (Exception ex)
            {
                json.Success = false;
                json.Msg = "密码修改出错";
                throw ex;
            }
            return JsonHelper.ToJson(json);
        }
        #endregion

        #region 修改用户
        /// <summary>
        /// 根据用户编号，修改用户信息，得到操作结果
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public string UpdateUser(sys_UsersModel userModel)
        {
            JsonMessage json = new JsonMessage();
            DataSet ds = sys_UserDAL.GetUsersInfoByWhere(" UserName='" + userModel.UserName + "' and UserId !=" + userModel.UserId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                json.Success = false;
                json.Msg = "用户名已经被别人占用，请更换";
            }
            else
            {
                int number = sys_UserDAL.UpdateUser(userModel);
                if (number > 0)
                {
                    json.Success = true;
                    json.Msg = "用户修改成功";
                }
                else if (number == 0)
                {
                    json.Success = false;
                    json.Msg = "用户修改失败";
                }
                else
                {
                    json.Success = false;
                    json.Msg = "用户修改出错";
                }
            }
            return JsonHelper.ToJson(json);
        }
        #endregion
        #region 修改用户状态
        /// <summary>
        /// 根据用户编号修改用户状态
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public string UpdateUserState(int userId)
        {
            JsonMessage json = new JsonMessage();
            int number = sys_UserDAL.DeleteByUserId(userId);
            if (number > 0)
            {
                json.Success = true;
                json.Msg = "用户操作成功";
            }
            else if (number == 0)
            {
                json.Success = false;
                json.Msg = "该用户不存在";
            }
            else
            {
                json.Success = false;
                json.Msg = "用户操作失败";
            }
            return JsonHelper.ToJson(json);
        }

        #endregion
        #endregion

        #region 查询操作

        #region 根据ID查询
        /// <summary>
        /// 根据用户编号，查询该用户信息
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns></returns>
        public string GetUserModelByUserId(int uid)
        {
            sys_UsersModel userModel = sys_UserDAL.GetUsersModelByUserId(uid);
            return JsonHelper.ToJson(userModel);
        }
        #endregion
        #region SQL查询所有用户
        /// <summary>
        /// 查询所有用户信息，得到Json数据
        /// </summary>
        /// <returns></returns>
        public string GetAllUsers()
        {
            DataSet ds = sys_UserDAL.GetUsersInfoByWhere("");
            StringBuilder json = new StringBuilder();
            json.Append("{\"total\":" + ds.Tables[0].Rows.Count + ",\"rows\":");
            json.Append(JsonHelper.ToJson(ds.Tables[0]));
            json.Append("}");
            return json.ToString();
        }
        #endregion
        #region 分页查询用户
        /// <summary>
        /// 使用分页查询存储过程进行数据查询
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="pageNo">当前页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordTotal">总行数</param>
        /// <returns></returns>
        public string GetUsersByProc(string strWhere,int pageNo,int pageSize,out int recordTotal)
        {
            DataTable dt = sys_UserDAL.GetUsersInfoByProc(strWhere,pageNo,pageSize,out recordTotal);
            StringBuilder strJson = new StringBuilder();
            strJson.Append("{\"total\":"+recordTotal+",\"rows\":");
            strJson.Append(JsonHelper.ToJson(dt));
            strJson.Append("}");
            return strJson.ToString();
        }
        #endregion
        #endregion





    }
}
