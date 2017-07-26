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

namespace test1
{

    /*
        此类专门用于目录的监听
     */
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //------------------------------大型对象变量区--------------------------------------------------------------
        //设定权限集合变量--用于记录选项框的内容
        private List<int> listCheckBoxs;

        //设定文件监听采集集合变量
        private List<FileBean> listFileLoads;

        //设定监听的新对象--防止产生缓存
        private FileSystemWatcher fileSystemWatcher;



        //------------------------------常量区--------------------------------------------------------------
        //设定文件过滤常量
        private const string FILE_FILTER = "*.*";
        
        //设定文件被创建了常量
        private const String FILE_CRETEA = "创建";

        //设定文件被删除了常量
        private const String FILE_DELETE = "删除";

        //设定文件被修改了常量
        private const String FILE_UPDATE = "修改";




        //------------------------------变量区--------------------------------------------------------------
        //设定监听的坐标变量
        private int fileIndex = 1024;

        //设定每一个file监听的序号
        private int fileID = 1;

        //设定判断是否start的变量
        private Boolean isStart = true;

        //设定文件监听路径变量
        private String filePath = "";

        //设定集合是否为空的变量--点击清空或者导出之后再次改为false
        private Boolean isListFile = false;

        //设定默认发送文件的地址路径变量
        private String xmlFilePathSet = "E:\\" + DateTime.Now.ToString("yy_MM_dd_hh_mm_ss") + "_test.xml";





        /*-----------------------------自定义方法区-----------------------------------------------
           自定义方法
         
         */

        /*
            此方法用于生成xml文件
         * xmlFilePath ：xml路径
         */
        private void saveXml(String xmlFilePath) {
            XmlDocument xmlDocument = new XmlDocument();

            MessageBox.Show("text :-----------------------------");

            //根节点
            XmlElement xmlGrad = xmlDocument.CreateElement("FileFilterXmls");
            xmlDocument.AppendChild(xmlGrad);

            foreach(FileBean fileBean in listFileLoads){
                //第一个节点s
                XmlElement xmlFather = xmlDocument.CreateElement("FileFilterXml");
                xmlGrad.AppendChild(xmlFather);

                XmlElement xmlFileId = xmlDocument.CreateElement("FileID");
                xmlFileId.InnerText = fileBean.FileID + "";
                xmlFather.AppendChild(xmlFileId);

                XmlElement xmlTime = xmlDocument.CreateElement("Time");
                xmlTime.InnerText = fileBean.Time;
                xmlFather.AppendChild(xmlTime);

                XmlElement xmlFileName = xmlDocument.CreateElement("FileName");
                xmlFileName.InnerText = fileBean.FileName;
                xmlFather.AppendChild(xmlFileName);

                XmlElement xmlType = xmlDocument.CreateElement("Type");
                xmlType.InnerText = fileBean.Type;
                xmlFather.AppendChild(xmlType);

            }
            MessageBox.Show("text :" + xmlFilePath);

            xmlDocument.Save(xmlFilePath);
        }


        /*
           私有方法
         * 目的是将重复的判断进行抽取
         * checkBox : 传递一个checkbox控件对象
         * checkInt ：选择的类型
         * 
        */
        private void isCheck(CheckBox checkBox, int checkInt)
        {
            if (checkBox.Checked)
            {
                listCheckBoxs.Add(checkInt);
            }
            else
            {
                listCheckBoxs.Remove(checkInt);
            }
        }


        /*
            发送启动程序，用于启动监听中的方法整合
         */
        private void sendFileFilter()
        {

            textFile.Text = filePath;

            fileSystemWatcher = new FileSystemWatcher();

            fileSystemWatcher.Path = filePath;
            fileSystemWatcher.Filter = FILE_FILTER;
            fileSystemWatcher.EnableRaisingEvents = true;

            //使用监听时，需要+=并new一个handler，传递一个处理事件--自定义时委托声明，触发事件
            foreach (int checkBox in listCheckBoxs)
            {
                if (checkBox == 1)
                {
                    fileSystemWatcher.Created += new FileSystemEventHandler(onPress);
                }
                if (checkBox == 2)
                {
                    fileSystemWatcher.Renamed += new RenamedEventHandler(onPress);
                }
                if (checkBox == 3)
                {
                    //浏览
                }
                if (checkBox == 4)
                {
                    fileSystemWatcher.Deleted += new FileSystemEventHandler(onPress);
                }
            }
        }

        /*
            公有方法
         * 目的是给filesystemwather一个处理方法
         * onject ：上下文对象
         * fileSystemEventArgs ：对应的filesystemeventargs委托
         * 
         */
        public void onPress(Object onject, FileSystemEventArgs fileSystemEventArgs)
        {
            switch (fileSystemEventArgs.ChangeType)
            {

                case WatcherChangeTypes.Created:
                    fileToBe(FILE_CRETEA, fileSystemEventArgs);
                    break;
                case WatcherChangeTypes.Deleted:
                    fileToBe(FILE_DELETE, fileSystemEventArgs);
                    break;
                case WatcherChangeTypes.Renamed:
                    fileToBe(FILE_UPDATE, fileSystemEventArgs);
                    break;
            }
        }


        /*
         *  抽取文件操作方法
         * fileTo : 文件的操作类型
         * fileSystemEventArgs ：事件委托体
         */
        private void fileToBe(String fileTo, FileSystemEventArgs fileSystemEventArgs)
        {
            if (listFileLoads == null)
            {
                listFileLoads = new List<FileBean>(fileIndex);
            }
            FileBean fileBean = new FileBean();
            fileBean.FileID = fileID;
            fileBean.FileName = fileSystemEventArgs.FullPath;
            fileBean.Time = DateTime.Now.ToString();
            fileBean.Type = fileTo;
            listFileLoads.Add(fileBean);
            fileID++;

            //去掉绑定
            GridView.DataSource = null;
            this.GridView.Rows.Add();
            int index = (int)listFileLoads.LongCount();
            FileBean fileBeanTo = listFileLoads[index - 1];

            GridView.Rows[index - 1].Cells[0].Value = fileBeanTo.FileID;
            GridView.Rows[index - 1].Cells[1].Value = fileBeanTo.Time;
            GridView.Rows[index - 1].Cells[2].Value = fileBeanTo.FileName;
            GridView.Rows[index - 1].Cells[3].Value = fileBeanTo.Type;

            log.Text = "1024条时自动导出到桌面-当前 :" + listFileLoads.LongCount();

            if (!isListFile)
            {
                if (listFileLoads.LongCount() > 0)
                {
                    isListFile = true;
                    clearBut.Enabled = true;
                    giveBut.Enabled = true;
                }
            }

            if(listFileLoads.LongCount() > 2){

                saveXml(xmlFilePathSet);

                MessageBox.Show("text :");
                isListFile = false;
                giveBut.Enabled = false;
                clearBut.Enabled = false;
                GridView.DataSource = new List<String>();
                listFileLoads = null;  
            }

        }

      





        /*-------------------------------------事件方法-----------------------------------------------------
           事件方法
         
         */
        /*
            窗体加载时触发
         *  在其中应该有gridview的加载操作
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            listCheckBoxs = new List<int>(4);
            startBut.Enabled = true;
            endBut.Enabled = false;
            giveBut.Enabled = false;
            clearBut.Enabled = false;
        }


        /*
         * 选择删除按钮时触发
         */
        private void deleteBox_CheckedChanged(object sender, EventArgs e)
        {
            isCheck(deleteBox, 4);
           
        }


        /*
           选择启动按钮时触发
         * 讲list中的数值取出进行判断
         * 启动监听文件事件
        */
        private void startBut_Click(object sender, EventArgs e)
        {
            if (isStart)
            {
                filePath = textFile.Text;
                if ("".Equals(filePath))
                {
                    MessageBox.Show("请选择需要监听的路径...");
                }
                else {
                    if (listCheckBoxs.LongCount() > 0)
                    {
                        if (listFileLoads == null)
                        {
                            listFileLoads = new List<FileBean>(fileIndex);
                        }
                        sendFileFilter();
                        isStart = false;
                        endBut.Enabled = true;
                        startBut.Enabled = false;
                        startBut.Text = "监听中...";
                    }
                    else
                    {
                        MessageBox.Show("请先选择监听类型...");
                    }
                }
                             
            }   
        }


        /*
           选择创建按钮时触发
        */
        private void createBox_CheckedChanged(object sender, EventArgs e)
        {
            isCheck(createBox, 1);
        }


        /*
         显示路径框
       */
        private void textFile_TextChanged(object sender, EventArgs e){}


        /*
        see按钮触发事件
      */
        private void lookBut_Click(object sender, EventArgs e)
        {
            //判断用户是否点击了浏览文件中的确定按钮
            if (folderDialogSee.ShowDialog() == DialogResult.OK)
            {
                textFile.Text = folderDialogSee.SelectedPath;
            }
        }


        /*
           选择修改按钮时触发
        */
        private void updateBox_CheckedChanged(object sender, EventArgs e)
        {
            isCheck(updateBox, 2);
        }


        /*
           选择浏览按钮时触发
        */
        private void seeBox_CheckedChanged(object sender, EventArgs e)
        {
            isCheck(seeBox, 3);
        }


        private void fileLSystemWatcher_Changed(object sender, System.IO.FileSystemEventArgs e){}

        private void GridView_CellContentClick(object sender, DataGridViewCellEventArgs e){}

        private void log_Click(object sender, EventArgs e)
        {

        }

        /*
            暂停按钮触发
         */
        private void endBut_Click(object sender, EventArgs e)
        {
            if (isStart)
            {
            }
            else {
                if (fileSystemWatcher == null)
                {
                    MessageBox.Show("是null");
                }
                fileSystemWatcher.EnableRaisingEvents = false; 
                isStart = true;
                startBut.Enabled = true;
                endBut.Enabled = false;
                startBut.Text = "启动";

                fileSystemWatcher = null;
            }
        }


        /*
            清空按钮被点击
         */
        private void clearBut_Click(object sender, EventArgs e)
        {
            listFileLoads = null;
            isListFile = false;
            giveBut.Enabled = false;
            clearBut.Enabled = false;
            GridView.DataSource = new List<String>();
        }

        /*
            导出到桌面
         */
        private void giveBut_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "数据标准格式(*.xml) | *.xml|数据简单格式(*.txt) | *.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String xmlFilePath = saveFileDialog.FileName;
                saveXml(xmlFilePath);
                isListFile = false;
                giveBut.Enabled = false;
                clearBut.Enabled = false;
                GridView.DataSource = new List<String>();
                listFileLoads = null;

            } 
        }
    }


   


    /*
        --------------------------------vo区---------------------------------------------------
     
     */
    /*
        此类vo类，用于存储gridview中显示的item
     */
    class FileBean{
        //private String fileName;
        private String time;
        private String type;
        private int fileID;

        public FileBean() { }

        public FileBean(int fileID,string fileName, String time, string type)
            {
                //this.fileName = fileName;
                this.FileName = fileName;
                this.fileID = fileID;
                this.time = time;
                this.type = type;
            }
        public string FileName { get; set; }
        //public String FileName {
        //    set {
        //        fileName = value;
        //    }
        //    get {
        //        return fileName;
        //    }
        
        //}

        public int FileID
        {
            set
            {
                fileID = value;
            }
            get
            {
                return fileID;
            }

        }

        public String Time
        {
            set
            {
                time = value;
            }
            get
            {
                return time;
            }

        }
        public String Type
        {
            set
            {
                type = value;
            }
            get
            {
                return type;
            }
        }
    }
}
