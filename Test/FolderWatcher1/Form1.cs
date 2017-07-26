using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace test
{



    public partial class Form1 : Form
    {
        String Path;
        FileSystemWatcher[] fswArr;
        FileSystemWatcher fsw = new FileSystemWatcher();
        private delegate void setLogTextDelegate(FileSystemEventArgs e);

        private delegate void renamedDelegate(RenamedEventArgs e);

        public Form1()
        {
            InitializeComponent();
        }

        //
        private void button1_Click(object sender, EventArgs e)
        {
            ////初始化一个OpenFileDialog类
            //OpenFileDialog fileDialog = new OpenFileDialog();

            //fileDialog.ShowDialog();
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.ShowDialog();
            Path = fb.SelectedPath;

            textBox1.Text = Path;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        //导出xml
        private void button2_Click(object sender, EventArgs e)
        {
            //String id = String.Empty;
            //String time = String.Empty;
            //String file = String.Empty;
            //String operation = String.Empty;
            XmlDocument doc = new XmlDocument();
            XmlElement xe = doc.CreateElement("root");
            dataGridView1.SelectAll();

            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    //
                    string id = this.dataGridView1.Rows[i].Cells[0].Value.ToString();
                    string time = this.dataGridView1.Rows[i].Cells[1].Value.ToString();
                    string file = this.dataGridView1.Rows[i].Cells[2].Value.ToString();
                    string operation = this.dataGridView1.Rows[i].Cells[3].Value.ToString();
                    doc.AppendChild(xe);
                    XmlElement department = doc.CreateElement("item");
                    department.SetAttribute("序号", id);
                    department.SetAttribute("时间", time);
                    department.SetAttribute("文件", file);
                    department.SetAttribute("操作", operation);
                    xe.AppendChild(department);

                }
            }
            catch (Exception ee)
            {

            }
            finally
            {
                try
                {   //xml路径选择
                    doc.Save(textBox2.Text+"\\folder.xml");
                }
                catch(Exception we){}
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

          
            if (!string.IsNullOrEmpty(this.textBox1.Text))
            {

            //    fsw = null;
                fsw.Path = Path;                    //监控的文件目录  
                //       fsw.IncludeSubdirectories = true;   //设置监控目录下的所有子目录  
                fsw.Filter = "*.txt";                   //设置监控文件的类型
                //设置文件的文件名、目录名及文件的大小改动会触发Changed事件 
                // fsw.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Size;

                    fsw.Created += new FileSystemEventHandler(this.fsw_Created); 

                    fsw.Changed += new FileSystemEventHandler(this.fsw_Changed);
                    fsw.Renamed += new RenamedEventHandler(this.fsw_Renamed); //重命名事件与增删改传递的参数不一样。

                  fsw.Deleted += new FileSystemEventHandler(this.fsw_Changed); 

                fsw.EnableRaisingEvents = true;
            }
            else
            {
                MessageBox.Show("未选择路径！");

                return;
            }


        }

        void fsw_Created(object sender, FileSystemEventArgs e)
        {
            if (this.dataGridView1.InvokeRequired)  //判断是否跨线程  
            {
                this.dataGridView1.Invoke(new setLogTextDelegate(setLogText), new object[] { e });  //使用委托将方法封送到UI主线程处理  
            }


        }

        void fsw_Renamed(object sender, RenamedEventArgs e)
        {
            if (this.dataGridView1.InvokeRequired)  //判断是否跨线程  
            {
                this.dataGridView1.Invoke(new renamedDelegate(setRenamedLogText), new object[] { e });  //使用委托将方法封送到UI主线程处理  
            }
        }


        //增加文件调用的方法
        void fsw_Changed(object sender, FileSystemEventArgs e)
        {

            if (this.dataGridView1.InvokeRequired)  //判断是否跨线程  
            {
                this.dataGridView1.Invoke(new setLogTextDelegate(setLogText), new object[] { e });  //使用委托将方法封送到UI主线程处理  
            }


        }



        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = Path;
            fsw.EnableRaisingEvents = false;

            foreach (Control cl in this.Controls)//循环整个form上的控件
            {
                if (cl is CheckBox)//看看是不是checkbox
                {
                    CheckBox ck = cl as CheckBox;//将找到的control转化成checkbox
                    ck.Enabled = true;
                }
            }
        }

        private void setLogText(FileSystemEventArgs e)  //更新UI界面  
        {
            //        DataGridViewRow row = new DataGridViewRow();
            int index = dataGridView1.Rows.Add();
            //         this.dataGridView1.Rows.Add();  
            this.dataGridView1.Rows[index].Cells[0].Value = index + 1;
            this.dataGridView1.Rows[index].Cells[1].Value = DateTime.Now;
            this.dataGridView1.Rows[index].Cells[2].Value = e.Name;
            this.dataGridView1.Rows[index].Cells[3].Value = e.ChangeType;
        }
        
        private void setRenamedLogText(RenamedEventArgs e)  //更新UI界面  
        {
            int index = dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].Cells[0].Value = index + 1;
            this.dataGridView1.Rows[index].Cells[1].Value = DateTime.Now;
            this.dataGridView1.Rows[index].Cells[2].Value = e.OldName + "->" + e.Name;
            this.dataGridView1.Rows[index].Cells[3].Value = e.ChangeType;//受影响的文件的改动类型（Rename）
        }
        
        //Creaeted
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

 
        //清空
        private void button3_Click(object sender, EventArgs e)
        {
          
            dataGridView1.Rows.Clear();
          
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
       
        //导出xml路径选择
        private void button6_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog open = new FolderBrowserDialog();
            DialogResult dr = open.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
                textBox2.Text = open.SelectedPath;

        }
        //路径显示
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
