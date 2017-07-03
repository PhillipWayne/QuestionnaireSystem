using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuestionnaireSystem.Common
{
    [Serializable]
    public class JsonMessage
    {
        public JsonMessage() { }
        public Boolean Success { get; set; }//操作是否成功
        public String Msg { get; set; }//提示信息
        public Object Obj { get; set; }//其他信息
    }
}
