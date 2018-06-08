using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data.Common;
using System.Windows.Forms;
using System.IO;
using System.Data;
using SQLiante1;
using System.Data.SqlClient;

namespace DBManage
{
    public class CodeConfig//内部类
    {
        public string m_Scaner_COM_config { get; set; }
        public string m_TestMachine_Config { get; set; }
        public string m_PLC_IP_config { get; set; }
    }
    public class DBManageHelper//数据库使用助手（自定义类）
    {
        public static SQLiteConnection GetConnection()//连接数据库方法(SQLiteConnection对象）
        {
           string  connStr = m_strdbpath;//声明一个用于储存连接数据库的字符串
            SQLiteConnection conn = new SQLiteConnection(connStr); //创建连接对象

            conn.Open();//打开连接
            return conn;
        }
       
     
        //保存条码配置 保存配置数据
        public static void SaveBarCode(CodeConfig cctem)
        {
            using (SQLiteConnection conn = GetConnection())//调用连接数据库的函数
            {
                //使用事务删除
                DbTransaction trans = conn.BeginTransaction();

                try
                { 
                    DbCommand cmd = conn.CreateCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = string.Format("INSERT INTO ConfigCode(strScaner,strTestMachine,strPLC) VALUES('{0}','{1}','{2}')",
                        cctem.m_Scaner_COM_config, cctem.m_TestMachine_Config, cctem.m_PLC_IP_config);
                    cmd.ExecuteNonQuery();
                    int k = 0;

                }
                catch (Exception ex)
                {
                    trans.Rollback();//回滚
                    return;
                }

                trans.Commit();//提交
            }
        }
    

        //加载条码数据
        public static void LoadBarCode(ref DataSet dataset)
        {
            
            //barcodeset.Clear();
            using (SQLiteConnection conn = GetConnection())
            {
                try
                {
                    string sql = string.Format("select * from ConfigCode");
                    //using (SQLiteDataAdapter ap = new SQLiteDataAdapter(sql, conn))
                    //{
                        
                        SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                        SQLiteDataAdapter ap = new SQLiteDataAdapter(cmd);
                        //SQLiteDataReader reader = cmd.ExecuteReader();//使用SQLiteDataReader方法创建ExecuteReader对象
                        //while (reader.Read())
                        //{
                        //    string strScaner = Convert.ToString(reader[0]);
                        DataSet ds = new DataSet();//创建一个DataSet对象
                        ap.Fill(ds, "ConfigCode");
                        dataset = ds;     
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        ////删除条码配置,
        static public void DeleteBarCode(string strtime)
        {
            using (SQLiteConnection conn = GetConnection())
            {
                try
                {
                    string sql = string.Format("delete from ConfigCode where id = '{0}' ",strtime);
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        static public void DeleteAllBarCode()
        {
            using (SQLiteConnection conn = GetConnection())
            {
                try
                {
                    string sql = string.Format("delete from ConfigCode ");
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    int ncount = cmd.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }


        static public void UpdateBarCode(string str1, string str2, string str3, string str4)
        {
            using (SQLiteConnection conn = GetConnection())
            {
                try
                {
                    string sql = string.Format("update ConfigCode set strScaner  = '{0}', strTestMachine = '{1}', strPLC = '{2}' where id = '{3}' ", str1, str2, str3, str4);
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }
   
        //数据库文件路径
        static private string m_strdbpath = "Data Source =" + Application.StartupPath + "\\Data.db";
        
       
    }
}
