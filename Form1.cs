using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace BossTime
{
    public partial class Form1 : Form
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private DataSet dataSet;
        private DataTable dataTable;
        private int len;
        private Label idlabel;
        private Label namelabel;
        private Label DeadTimelabel;


        public Form1()
        {
            InitializeComponent();
            dataSet = new DataSet();
            string dbHost = "sql12..net";//資料庫位址
            string dbUser = "";//資料庫使用者帳號
            string dbPass = "";//資料庫使用者密碼
            string dbName = "";//資料庫名稱
            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            conn = new MySqlConnection(connStr);
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Boss_time";
            conn.Open();
            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            adap.Fill(dataSet, "bosstime");
            dataTable = dataSet.Tables["bosstime"];
            conn.Close();


            len = dataTable.Rows.Count;
            for (int i=0;i<len;i++)
            {
                // add id label
                int idlabel_len = 17;
                idlabel = new Label();
                //int count = panel1.Controls.OfType<Label>().ToList().Count;
                //namelabel.Location = new Point(10, (20 * count) + 2);
                idlabel.Location = new Point(0, (20 * i) + 2);
                idlabel.Size = new Size(idlabel_len, 15);
                idlabel.Name = "idlabel" + (i + 1);
                idlabel.Text = dataTable.Rows[i]["id"].ToString();

                // add name label
                namelabel = new Label();
                //int count = panel1.Controls.OfType<Label>().ToList().Count;
                //namelabel.Location = new Point(10, (20 * count) + 2);
                namelabel.Location = new Point(idlabel_len + 10, (20 * i) + 2);
                namelabel.Size = new Size(80, 15);
                namelabel.Name = "namelabel_" + (i + 1);
                namelabel.Text = dataTable.Rows[i]["name"].ToString();
                //label.Text = "label " + (count + 1);

                // add dead time
                DeadTimelabel = new Label();
                DeadTimelabel.Location = new Point(idlabel_len + 10 + 80, (20 * i) + 2);
                DeadTimelabel.Size = new Size(200, 15);
                DeadTimelabel.Name = "DeadTimelabel_" + (i + 1);
                DeadTimelabel.Text = dataTable.Rows[i]["DeadTime"].ToString();
                
                panel1.Controls.Add(idlabel);
                panel1.Controls.Add(namelabel);
                panel1.Controls.Add(DeadTimelabel);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            try
            {
                //Console.WriteLine("id={0} , name={1}", dataTable.Rows[3]["id"], dataTable.Rows[3]["name"]);
                //label1.Text = dataTable.Rows[3]["name"].ToString();
                //Console.WriteLine(len);

                cmd.CommandText = "SELECT * FROM Boss_time";
                conn.Open();
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                adap.Fill(dataSet, "bosstime");
                dataTable = dataSet.Tables["bosstime"];
                conn.Close();
                UpdateLabels();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Clone();
                }

            }

        }

        private void UpdateLabels ()
        {
            len = dataTable.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                idlabel.Text = dataTable.Rows[i]["id"].ToString();
                namelabel.Text = dataTable.Rows[i]["name"].ToString();
                DeadTimelabel.Text = dataTable.Rows[i]["DeadTime"].ToString();
            }
        }

    }
}
