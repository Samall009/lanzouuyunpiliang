using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace 蓝奏云批量上传
{
    class Login
    {
        // 用户名
        private string UserName;

        /**
         * 获取用户名
         */
        public string getUserName()
        {
            return this.UserName;
        }

        // 密码
        private string PasswWord;

        /**
         * 获取密码
         */
        public string getPasswWord()
        {
            return this.PasswWord;
        }

        // 登陆状态
        private Boolean LogStatus = false;

        /**
         * 获取登陆状态
         */
        public Boolean getLogStatus()
        {
            return this.LogStatus;
        }

        /**
         * 登陆初始化
         */
        public Login(string UserName, string password)
        {
            this.UserName = UserName;
            this.PasswWord = password;
        }

        public void showConfig()
        {
            MessageBox.Show("用户名: " + this.UserName);
            MessageBox.Show("密码: " + this.PasswWord);
        }

        // 蓝奏云Cookie
        private CookieContainer Cookie = new CookieContainer();

        public CookieContainer GetCookieContainer()
        {
            return this.Cookie;
        }

        /*
         * 登陆蓝奏云
         */
        public Boolean LoinLanZou()
        {
            string URL = "https://up.woozooo.com/account.php";
            
            this.Cookie = this.GetCookie(URL, this.UserName, this.PasswWord);

            if (this.Cookie.Count <= 1)
            {
                MessageBox.Show("登陆失败,请检查账号密码是否正确!");
                return false;
            }

            // 修改登陆状态为正常
            this.LogStatus = true;

            return true;
        }




        /**
         * 
         * 获取蓝奏云Cookie
         */
        private CookieContainer GetCookie(string url, string userName, string pwd)
        {
            CookieContainer cookie = new CookieContainer();
            // 装填数据
            Dictionary<string, string> postData = new Dictionary<string, string>();
            // 操作类型
            postData.Add("action", "login");
            postData.Add("task", "login");
            // 表单哈希值
            postData.Add("formhash", "002b2898");
            // 用户名
            postData.Add("username", userName);
            // 用户密码
            postData.Add("password", pwd);
            // 发送POST请求
            string responseFromServer = NetWork.Post(url, postData, cookie);
            // 控制台打印
            Console.WriteLine("返回数据" + responseFromServer);
            Console.ReadLine();
            // 返回数据
            return cookie;
        }
    }
}
