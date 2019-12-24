using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 蓝奏云批量上传
{
    class ReturnJson
    {
        /// <summary>
        /// 
        /// </summary>
        public int zt { get; set; }
        /// <summary>
        /// 上传成功
        /// </summary>
        public string info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<TextItem> text { get; set; }
    }

    public class TextItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string f_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name_all { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string size { get; set; }
        /// <summary>
        /// 0 秒前
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string downs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string onof { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_newd { get; set; }
    }
}
