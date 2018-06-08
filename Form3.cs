using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using COMtest;
using DBManage;
using System.Data.SQLite;

namespace SQLiante1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public static string m_strpath = Application.StartupPath + "\\SystemConfig.xml";//获得文件路径
        parse_config temp = new parse_config(m_strpath);//实例化类，并初始化



        private void button2_Click(object sender, EventArgs e)
        {
            temp.loadfile();
            textBox1.ReadOnly = false;
            textBox1.Text = temp.m_Scaner_COM_config;
            textBox2.ReadOnly = false;
            textBox2.Text = temp.m_TestMachine_Config;
            textBox3.ReadOnly = false;
            textBox3.Text = temp.m_PLC_IP_config;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBManage.CodeConfig cctem = new DBManage.CodeConfig();
            cctem.m_Scaner_COM_config = textBox1.Text;
            cctem.m_TestMachine_Config = textBox2.Text;
            cctem.m_PLC_IP_config = textBox3.Text;
            DBManageHelper.SaveBarCode(cctem);
             temp.loadfile();
             cctem.m_Scaner_COM_config = temp.m_Scaner_COM_config;
             cctem.m_TestMachine_Config = temp.m_TestMachine_Config;
             cctem.m_PLC_IP_config = temp.m_PLC_IP_config;

             textBox1.Clear();
             textBox2.Clear();
             textBox3.Clear();

        }
    }

        }
    

