using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace UpFileClient
{
    public partial class Class1
    {
        public bool ProcessRequest(string filePath, string sjlj, string fileName)

        {

            //参考http://www.cnblogs.com/greenerycn/archive/2010/05/15/csharp_http_post.html  

            // 边界符  

            var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");

            var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // 最后的结束符  

            var endBoundary = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");

            // 文件参数头  

            const string filePartHeader =

                "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +

                "Content-Type: application/octet-stream\r\n\r\n";

            var fileHeader = string.Format(filePartHeader, "file", fileName);

            var fileHeaderBytes = Encoding.UTF8.GetBytes(fileHeader);

            // 开始拼数据  

            var memStream = new MemoryStream();

            memStream.Write(beginBoundary, 0, beginBoundary.Length);

            // 文件数据  

            memStream.Write(fileHeaderBytes, 0, fileHeaderBytes.Length);

            var buffer = new byte[1024];

            int bytesRead; // =0  

            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)

            {

                memStream.Write(buffer, 0, bytesRead);

            }

            fileStream.Close();

            //Key - Value数据

            var stringKeyHeader = "\r\n--" + boundary +

                                   "\r\nContent-Disposition: form-data; name=\"{0}\"" +

                                   "\r\n\r\n{1}\r\n";

            //此处是接口所需的其他参数

            Dictionary<string, string> stringDict = new Dictionary<string, string>();

            stringDict.Add("reportPath", sjlj);

            stringDict.Add("reportName", fileName);

            foreach (byte[] formitembytes in from string key in stringDict.Keys

                                             select string.Format(stringKeyHeader, key, stringDict[key])

                                                 into formitem

                                             select Encoding.UTF8.GetBytes(formitem))

            {

                memStream.Write(formitembytes, 0, formitembytes.Length);

            }

            // 写入最后的结束边界符  

            memStream.Write(endBoundary, 0, endBoundary.Length);

            //倒腾到tempBuffer

            memStream.Position = 0;

            var tempBuffer = new byte[memStream.Length];

            memStream.Read(tempBuffer, 0, tempBuffer.Length);

            memStream.Close();

            // 创建webRequest并设置属性  

            var webRequest = (HttpWebRequest)WebRequest.Create("");

            webRequest.Method = "POST";

            webRequest.Timeout = 100000;

            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;

            webRequest.ContentLength = tempBuffer.Length;

            webRequest.KeepAlive = false;

            webRequest.ProtocolVersion = HttpVersion.Version10;

            webRequest.Headers.Add("Authorization", "");

            var requestStream = webRequest.GetRequestStream();

            requestStream.Write(tempBuffer, 0, tempBuffer.Length);

            requestStream.Close();

            var httpWebResponse = (HttpWebResponse)webRequest.GetResponse();

            string responseContent;

            var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));

            responseContent = httpStreamReader.ReadToEnd();

            httpWebResponse.Close();

            webRequest.Abort();

            //接口返回值

            if (responseContent.ToUpper().Equals("TRUE"))

            {

                return true;

            }

            return false;

        }


    }
}