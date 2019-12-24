using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 蓝奏云批量上传
{
    class UploadType
    {
        /// <summary>
        /// 无参构造
        /// </summary>
        public UploadType()
        {
            FileNameKey = "upload_file";
            Encoding = Encoding.UTF8;
            PostData = new Dictionary<string, string>();
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件名称key
        /// </summary>
        public string FileNameKey { get; set; }

        /// <summary>
        /// 文件名称value
        /// </summary>
        public string FileNameValue { get; set; }

        /// <summary>
        /// 编码格式
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// 上传文件的流
        /// </summary>
        public FileStream UploadStream { get; set; }

        /// <summary>
        /// 上传文件 携带的参数集合
        /// </summary>
        public IDictionary<string, string> PostData { get; set; }

        /// <summary>
        /// 文件列表对应ViewItem
        /// </summary>
        public ListViewItem ViewItem { get; set; }

        /// <summary>
        /// 登陆信息
        /// </summary>
        public Login Login { get; set; }

        /// <summary>
        /// 文件type信息
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 文件type信息
        /// </summary>
        public string FileTypeExtension { get; set; }
    }
}
