using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace 蓝奏云批量上传
{
    class FileHelp
    {
        /*
         * 文件操作对象
         */
        private DirectoryInfo directoryInfo;

        static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /*
         * 带参构造
         */
        public FileHelp(string rootPath)
        {
            // 获取文件对象
            directoryInfo = new DirectoryInfo(rootPath);
        }

        public FileHelp()
        {

        }

        /*
         * 获取文件
         */
        public FileInfo[] fileInfos()
        {
            return directoryInfo.GetFiles();
        }

        public ArrayList getListViewItem()
        {
            // 视图对象
            ListViewItem listViewItem = new ListViewItem();

            // 创建动态数据
            ArrayList arrayList = new ArrayList();

            if (null == directoryInfo.GetFiles())
            {
                MessageBox.Show("文件夹为空");
                return arrayList;
            }

            // 标点
            int index = 0;

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                // 克隆函数
                ListViewItem newListViewItem = (ListViewItem)listViewItem.Clone();

                // 列表
                newListViewItem.ImageIndex = index;

                // 文件名称
                newListViewItem.Text = file.Name;
                // 文件路径
                newListViewItem.SubItems.Add(file.FullName);
                // 文件大小
                newListViewItem.SubItems.Add(CountSize(file.Length));
                // 状态
                newListViewItem.SubItems.Add("×");
                // 上传进度
                newListViewItem.SubItems.Add("0%");

                arrayList.Add(newListViewItem);
            }

            return arrayList;
        }

        /*
         * 
         * 获取文件大小
         */
        public static string CountSize(long Size)
        {
            string m_strSize = "";
            long FactSize = 0;
            FactSize = Size;
            if (FactSize < 1024.00)
                m_strSize = FactSize.ToString("F2") + " Byte";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = (FactSize / 1024.00).ToString("F2") + " K";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F2") + " M";
            else if (FactSize >= 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00 / 1024.00).ToString("F2") + " G";
            return m_strSize;
        }

        private static string GetType(string extension)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>() {
                { ".zip","application/x-zip-compressed"},
                { ".rar","application/octet-stream"},
                { ".txt","text/plain"},
                { ".7z","application/octet-stream"},
                { ".doc","application/msword"},
                { ".docx","application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                { ".xls","application/vnd.ms-excel"},
                { ".xlsx","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                { ".ppt","application/vnd.ms-powerpoint"},
                { ".pptx","application/vnd.openxmlformats-officedocument.presentationml.presentation"},
                { ".exe","application/x-msdownload"},
                { ".pdf","application/pdf"}
            };
            return dic[extension];
        }

        /// <summary>  
        /// 本地时间转成GMT格式的时间  
        /// </summary>  
        private static string ToGMTFormat(DateTime dt)
        {
            return dt.ToString("r") + dt.ToString("zzz").Replace(":", "");
        }

        ~FileHelp()
        {
            
        }

        /// <summary>
        /// 文件发送
        /// </summary>
        /// <param name="login"></param>
        /// <param name="listViewItem"></param>
        /// <param name="FilePath"></param>
        /// <param name="FileName"></param>
        public static void FileUpload(Login login, ListViewItem listViewItem, string FilePath, string FileName)
        {
            try
            {
                using (FileStream fileStream = File.OpenRead(FilePath))
                {
                    // 设置请求参数
                    Dictionary<string, string> PostData = new Dictionary<string, string>();
                    PostData.Add("task", "1");
                    PostData.Add("folder_id", "-1");
                    PostData.Add("id", "WU_FILE_0");
                    PostData.Add("name", FilePath);
                    PostData.Add("type", GetType(Path.GetExtension(FilePath)));
                    PostData.Add("lastModifiedDate", ToGMTFormat(DateTime.Now));
                    PostData.Add("size", fileStream.Length.ToString());
                    
                    UploadType uploadType = new UploadType
                    {
                        Login = login,
                        ViewItem = listViewItem,
                        FilePath = FilePath,
                        FileNameValue = FileName,
                        FileTypeExtension = GetType(Path.GetExtension(FilePath)),
                        UploadStream = fileStream,
                        PostData = PostData
                    };
                    
                    // 执行上传
                    NetWork.UploadFile(uploadType, "https://up.woozooo.com/fileup.php");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("上传错信息: " + ex.Message);
            }
        }

        public static void TestUpload(Login login)
        {
            // 测试文件
            string filePath = "C:\\Lucky\\App\\cpu-z.zip";
            // 链接对象
            string url = "https://up.woozooo.com/fileup.php";
            // 上传文件名
            string fileKeyName = "upload_file";

            using (MemoryStream memoryStream = new MemoryStream()) 
            {
                // 定义分节符
                // 分界线可以自定义参数
                string boundary = string.Format("----{0}", DateTime.Now.Ticks.ToString("x")),
                        beginBoundary = string.Format("--{0}\r\n", boundary),
                        endBoundary = string.Format("\r\n--{0}--\r\n", boundary);

                // 将字符串编码为字节序列
                byte[] beginBoundaryBytes = Encoding.UTF8.GetBytes(beginBoundary),
                        endBoundaryBytes = Encoding.UTF8.GetBytes(endBoundary);

                // 组装开始分界线数据体 到内存流中
                memoryStream.Write(beginBoundaryBytes, 0, beginBoundaryBytes.Length);

                // 请求参数写入数据流
                Dictionary<string, string> PostData = new Dictionary<string, string>();
                PostData.Add("task", "1");
                PostData.Add("folder_id", "-1");
                PostData.Add("id", "WU_FILE_0");
                PostData.Add("name", Path.GetFileName(filePath));
                PostData.Add("type", GetType(Path.GetExtension(filePath)));
                PostData.Add("lastModifiedDate", ToGMTFormat(DateTime.Now));
                
                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    PostData.Add("size", fileStream.Length.ToString());
                }

                foreach (KeyValuePair<string, string> keyValuePair in PostData)
                {
                    string parameterHeaderTemplate = string.Format("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n{2}", keyValuePair.Key, keyValuePair.Value, beginBoundary);
                    byte[] parameterHeaderBytes = Encoding.UTF8.GetBytes(parameterHeaderTemplate);

                    memoryStream.Write(parameterHeaderBytes, 0, parameterHeaderBytes.Length);
                }

                // 文件类型名称
                string fileHeaderTemplate = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", fileKeyName, Path.GetFileName(filePath), GetType(Path.GetExtension(filePath)));
                byte[] fileHeaderBytes = Encoding.UTF8.GetBytes(fileHeaderTemplate);
                memoryStream.Write(fileHeaderBytes, 0, fileHeaderBytes.Length);

                // 组装文件流 到内存流中
                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    byte[] buffer = new byte[1024 * 1024 * 1];
                    int size = fileStream.Read(buffer, 0, buffer.Length);
                    while (size > 0)
                    {
                        memoryStream.Write(buffer, 0, size);
                        size = fileStream.Read(buffer, 0, buffer.Length);
                    }
                }
                // 组装结束分界线数据体 到内存流中
                memoryStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
                // 获取二进制数据
                byte[] postBytes = memoryStream.ToArray();
                // HttpWebRequest 组装
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url, UriKind.RelativeOrAbsolute));

                webRequest.Method = "POST";
                webRequest.Timeout = 20000;
                webRequest.ServicePoint.Expect100Continue = false;
                HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                webRequest.CachePolicy = noCachePolicy;
                ServicePoint currentServicePoint = webRequest.ServicePoint;
                currentServicePoint.ConnectionLimit = 65500;

                // 写入 ContentType 
                webRequest.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
                webRequest.KeepAlive = false;

                // 写入cookie
                webRequest.CookieContainer = login.GetCookieContainer();
                // 判断是否HTTPS请求
                if (Regex.IsMatch(url, "^https://"))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                    ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                }

                // 9.写入上传请求数据
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(postBytes, 0, postBytes.Length);
                    requestStream.Close();
                }

                // 10.获取响应
                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        string body = reader.ReadToEnd();
                        reader.Close();
                        Console.WriteLine("返回数据");
                        Console.WriteLine(body);
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}
