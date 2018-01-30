using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace Merchandise_Counter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string sql = "";
            GetProductDetails(sql);
        }
        void BuildDataGrid()
        {
            
        }
        private void GetProductDetails(string sql)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString.ToString();
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            MySqlDataAdapter adpt = new MySqlDataAdapter(sql, connection);
            dataclass dc = new dataclass();
            DataTable dt = new DataTable();
            dt = dc.GetDataSet("" , "");

            
        }

    }
}
