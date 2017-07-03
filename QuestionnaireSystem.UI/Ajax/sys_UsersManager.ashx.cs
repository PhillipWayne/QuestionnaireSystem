using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionnaireSystem.BLL;
using QuestionnaireSystem.Model;

namespace QuestionnaireSystem.UI.Ajax
{
    /// <summary>
    /// sys_UsersManager 的摘要说明
    /// </summary>
    public class sys_UsersManager : IHttpHandler
    {
        private readonly sys_UsersBLL usersBLL = new sys_UsersBLL();
        /// <summary>
        /// 接受http请求，进行处理并发送响应结构 httpcontext 请求文本/头文件
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request.QueryString["type"].ToString();
            string result = "";
            switch (type)
            {
                case "login":
                    result= Login(context);
                    break;
                case "pass":
                    result = UpdatePassword(context);
                    break;
                case "get":
                    result = GetAllUsers(context);
                    break;
                case "add":
                    result = AddUser(context);
                    break;
                case "edit":
                    result =GetUserModelByUserId(context);
                    break;
                case "update":
                    result = UpdateUser(context);
                    break;
                case "del":
                    result = UpdateUserState(context);
                    break;

            }           
            context.Response.Write(result);
        }
        private string Login(HttpContext context)
        {
            string name = context.Request.Form["name"].ToString();
            string password = context.Request.Form["password"].ToString();
            //调用业务逻辑层进行登录判断
            string result = usersBLL.Login(name, password);
            return result;
        }
        private string UpdatePassword(HttpContext context)
        {
            string uid = context.Request.Form["uid"].ToString();
            string mpass = context.Request.Form["mpass"].ToString();
            string newpass = context.Request.Form["newpass"].ToString();
            string result = usersBLL.UpdatePassword(uid,mpass,newpass);
            return result;
        }
        private string GetAllUsers(HttpContext context)
        {
            int pageNo = int.Parse(context.Request.QueryString["pno"].ToString());
            int pageSize = int.Parse(context.Request.QueryString["psize"].ToString());
            int total = 0;
            string result = usersBLL.GetUsersByProc("1=1",pageNo,pageSize,out total);
            return result;
        }
        private string AddUser(HttpContext context)
        {
            string result = "";
            try
            {
                sys_UsersModel userModel = new sys_UsersModel();
                userModel.UserName = context.Request.Form["username"].ToString();
                userModel.UserPassword = context.Request.Form["userpassword"].ToString();
                userModel.RealName = context.Request.Form["realname"].ToString();
                userModel.SubordinateEnterprise = int.Parse(context.Request.Form["subor"].ToString());
                userModel.UserType = int.Parse(context.Request.Form["usertype"].ToString());
                userModel.UserState = int.Parse(context.Request.Form["userstate"].ToString());
                result = usersBLL.AddUser(userModel);
            }
            catch (Exception)
            {
                result = "{\"Success\":\"false\",\"Msg\":\"用户信息类型转换出错\"}";
                throw;
            }

            return result;
        }

        private string GetUserModelByUserId(HttpContext context)
        {
            string uid = context.Request.QueryString["uid"].ToString();
            string result = usersBLL.GetUserModelByUserId(int.Parse(uid));
            return result;
        }

        private string UpdateUser(HttpContext context)
        {
            try
            {
                sys_UsersModel userModel = new sys_UsersModel();
                userModel.UserId = int.Parse(context.Request.Form["userid"].ToString());
                userModel.UserName = context.Request.Form["username"].ToString();
                userModel.UserPassword = context.Request.Form["userpassword"].ToString();
                userModel.RealName = context.Request.Form["realname"].ToString();
                userModel.SubordinateEnterprise = int.Parse(context.Request.Form["subor"].ToString());
                userModel.UserType = int.Parse(context.Request.Form["usertype"].ToString());
                userModel.UserState = int.Parse(context.Request.Form["userstate"].ToString());
                string result = usersBLL.UpdateUser(userModel);
                return result;
            }
            catch (Exception ex)
            {
                return "{\"Success\":\"False\",\"Msg\":\"用户信息类型转换出错\"}";
                throw;
            }

        }

        private string UpdateUserState(HttpContext context)
        {
            try
            {
                int userId = int.Parse(context.Request.QueryString["uid"].ToString());
                string result = usersBLL.UpdateUserState(userId);
                return result;
            }
            catch (Exception)
            {
                return "{\"Success\":\"False\",\"Msg\":\"该用户编号存在错误\"}";
                throw;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}