using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace 蓝奏云批量上传
{
    class NetWork
    {
        /**
         * 发送POST请求
         */
        public static string Post(string url, Dictionary<string, string> dic, CookieContainer cookie)
        {
            // 创建Http对象
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            // 设置请求参数
            req.Method = "POST";
            // 数据类型
            req.ContentType = "application/x-www-form-urlencoded";
            // 容器信息
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
            // HTTP 版本
            req.ProtocolVersion = HttpVersion.Version11;
            // 允许自动重定向
            req.AllowAutoRedirect = true;
            // 设置cookie信息
            req.CookieContainer = cookie;
            // 超时时长
            req.Timeout = 10000;
            // 检修测试点
            req.ServicePoint.Expect100Continue = false;
            // 获取数据
            byte[] data = GetBytes(dic);
            // 设置请求内容长度
            req.ContentLength = data.Length;

            // 获取用于写入请求数据的 System.IO.Stream 对象
            using (Stream reqStream = req.GetRequestStream())
            {
                // 写入数据
                reqStream.Write(data, 0, data.Length);
                // 关闭流对象
                reqStream.Close();
            }

            // 获取网络响应 并装换为 System.Net.HttpWebResponse 类的新实例
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            // 获取流，该流用于读取来自服务器的响应的体
            Stream stream = resp.GetResponseStream();
            // 定义空字符串
            string result = "";
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                // 读取所有数据
                result = reader.ReadToEnd();
                // 关闭流
                reader.Close();
            }
            // 关闭流
            stream.Close();
            resp.Close();
            // 返回
            return result;
        }

        /*
         * 获取字节数据
         */
        private static byte[] GetBytes(Dictionary<string, string> data)
        {
            // 字符串集合 添加POST数据
            StringBuilder builder = new StringBuilder();
            // 首次字符串
            bool isUp = true;
            foreach (var item in data)
            {
                if (!isUp)
                {
                    // 除了第一次后其余都加上分隔符
                    builder.Append("&");
                }
                // 添加数据键值对
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                isUp = false;
            }
            // 将字符串中的所有字符编码为一个字节序列
            byte[] byteData = Encoding.UTF8.GetBytes(builder.ToString());

            // 返回字符集
            return byteData;
        }

        public static void UploadFile(UploadType uploadType, string Url)
        {
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
                
                foreach (KeyValuePair<string, string> keyValuePair in uploadType.PostData)
                {
                    string parameterHeaderTemplate = string.Format("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n{2}", keyValuePair.Key, keyValuePair.Value, beginBoundary);
                    byte[] parameterHeaderBytes = Encoding.UTF8.GetBytes(parameterHeaderTemplate);

                    memoryStream.Write(parameterHeaderBytes, 0, parameterHeaderBytes.Length);
                }

                // 文件类型名称
                string fileHeaderTemplate = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", uploadType.FileNameKey, uploadType.FileNameValue, uploadType.FileTypeExtension);
                byte[] fileHeaderBytes = Encoding.UTF8.GetBytes(fileHeaderTemplate);
                memoryStream.Write(fileHeaderBytes, 0, fileHeaderBytes.Length);

                // 组装文件流 到内存流中
                byte[] buffer = new byte[1024 * 1024 * 1];
                int size = uploadType.UploadStream.Read(buffer, 0, buffer.Length);
                while (size > 0)
                {
                    memoryStream.Write(buffer, 0, size);
                    size = uploadType.UploadStream.Read(buffer, 0, buffer.Length);
                }

                // 组装结束分界线数据体 到内存流中
                memoryStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
                // 获取二进制数据
                byte[] postBytes = memoryStream.ToArray();
                // HttpWebRequest 组装
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(Url, UriKind.RelativeOrAbsolute));

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
                webRequest.CookieContainer = uploadType.Login.GetCookieContainer();
                // 判断是否HTTPS请求
                if (Regex.IsMatch(Url, "^https://"))
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

                        ReturnJson ReturnJson = JsonConvert.DeserializeObject<ReturnJson>(body);

                        uploadType.ViewItem.SubItems[3].Text = ReturnJson.zt == 1 ? "√" : "X";
                        uploadType.ViewItem.SubItems[4].Text = ReturnJson.info;                        

                        reader.Close();
                    }
                }
            }
        }

        static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        private static string ToGMTFormat(DateTime now)
        {
            throw new NotImplementedException();
        }
    }
}
