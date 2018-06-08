using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBManage;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Xml;

namespace SQLiante1
{

    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            DBManageHelper.UpdateBarCode(textBox1.Text, textBox2.Text, textBox3.Text, numericUpDown1.Text);
            UIDisplay();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strtime = numericUpDown1.Value.ToString();//在sql语句中实现读取控件的值
            DBManageHelper.DeleteBarCode(strtime);
            UIDisplay();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DBManageHelper.DeleteAllBarCode();
            UIDisplay();
        }
        public void UIDisplay()
        {
            List<CodeConfig> barcodeset = new List<DBManage.CodeConfig>();
            DataSet dataset = new DataSet();//实例化DataSet
            DBManageHelper.LoadBarCode(ref dataset);
            dataGridView1.DataSource = dataset.Tables[0];//数据库表中的数据显示在界面
        }


        private void Form4_Load(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = 96;
            numericUpDown1.Minimum = 0;
            UIDisplay();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            label4.Text = "当前控件显示的数值： " + numericUpDown1.Value;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;//在dataGridView控件中的事件实现单击某条数据显示详细信息
            numericUpDown1.Text = dataGridView1.Rows[r].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[r].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[r].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[r].Cells[3].Value.ToString();
        }


        public string str4 { get; set; }

        private void button5_Click(object sender, EventArgs e)
        {
            CreateXmlFile();
        }

        public void CreateXmlFile()
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建根节点  
            XmlNode root = xmlDoc.CreateElement("Users");
            xmlDoc.AppendChild(root);

            XmlNode node1 = xmlDoc.CreateNode(XmlNodeType.Element, "User", null);
            CreateNode(xmlDoc, node1, "扫码器配置", textBox1.Text);
            CreateNode(xmlDoc, node1, "测试仪配置", textBox2.Text);
            CreateNode(xmlDoc, node1, "plc配置", textBox3.Text);
            root.AppendChild(node1);

            try
            {
                xmlDoc.Save("c://data5.xml");
            }
            catch (Exception e)
            {
                //显示错误信息  
                Console.WriteLine(e.Message);
            }
           
        }

        public void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CreateXmlFile1();
        }

        public void CreateXmlFile1()
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建根节点  
            XmlNode root = xmlDoc.CreateElement("Users");
            xmlDoc.AppendChild(root);
            XmlNode node1 = xmlDoc.CreateNode(XmlNodeType.Element, "User", null);
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {

                CreateNode(xmlDoc, node1, "扫码器配置", dataGridView1.Rows[i].Cells[1].Value.ToString());
                CreateNode(xmlDoc, node1, "测试仪配置", dataGridView1.Rows[i].Cells[2].Value.ToString());
                CreateNode(xmlDoc, node1, "plc配置", dataGridView1.Rows[i].Cells[3].Value.ToString());
                root.AppendChild(node1);
            }
           

            try
            {
                xmlDoc.Save("c://data5.xml");
            }
            catch (Exception e)
            {
                //显示错误信息  
                Console.WriteLine(e.Message);
            }
            //Console.ReadLine(); 
        }

       
    }
}
