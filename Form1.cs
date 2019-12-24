using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 蓝奏云批量上传
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void password_Click(object sender, EventArgs e)
        {

        }

        // 登陆类对象
        private Login Log;

        /*
         * 登陆按钮点击事件
         */
        private void login_Click(object sender, EventArgs e)
        {
            Log = new Login(this.userName.Text, this.password.Text);
            // Log = new Login("17611356828", "zc654321.");

            // Log.showConfig();

            // 执行登陆
            bool isLog = Log.LoinLanZou();

            // 判断是否登陆成功
            if (isLog)
            {
                // 更改UI
                this.loginStatus.Text = "√";
                Color color = Color.FromName("Lime");
                this.loginStatus.ForeColor = color;
            }
        }

        /*
         * 选择文件目录
         */
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            dialog.Description = "请选择文件夹";
             
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string SelectedPath = dialog.SelectedPath;          // 获取当前选中目录
                this.loadPath.Text = SelectedPath;                  // 写入选中目录

                FileHelp fileHelp  = new FileHelp(SelectedPath);    // 获取选中目录文件对象

                ArrayList arrayList = fileHelp.getListViewItem();   // 获取目录下所有文件夹

                this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度

                this.listView1.Items.Clear();   // 清除列表中所有项目
    
                foreach (ListViewItem viewItem in arrayList)
                {
                    this.listView1.Items.Add(viewItem);     // 添加新项目到items中
                }

                this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
            }
        }

        /// <summary>
        /// 是否正在上传中
        /// </summary>
        private bool isLoad = false;

        private void update_Click(object sender, EventArgs e)
        {
            if (!Log.getLogStatus())
            {
                // 判断是否登陆
                MessageBox.Show("请执行登陆");
                return;
            }

            if (isLoad)
            {
                // 判断是否登陆
                MessageBox.Show("正在执行其他任务,请稍后~");
                return;
            }

            // 锁定状态
            isLoad = true;

            // 循环列表中的数据
            foreach (ListViewItem View in this.listView1.Items)
            {
                // 新增子线程
                FileHelp.FileUpload(Log, View, View.SubItems[1].Text, View.SubItems[0].Text);
            }

            // 任务完成
            isLoad = false;
        }
    }
}
