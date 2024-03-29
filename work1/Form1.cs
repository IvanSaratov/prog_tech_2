using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLExpress1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        static string GetConnectionString()
        {
            return @"Data Source=ups-anna\ssu_express;Initial Catalog=SSU_work_1; Integrated Security=SSPI;";
        }

        private void LoadData()
        {
            string connectionString = GetConnectionString();
            SqlConnection myConnection = new SqlConnection(connectionString);

            myConnection.Open();

            string query = "SELECT id, name, address FROM Enterprice ORDER BY name";

            SqlCommand command = new SqlCommand(query, myConnection);
            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[3]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
            }

            reader.Close();

            myConnection.Close();
            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }

        private void updateJobs(int enterpriceID)
        {
            string connectionString = GetConnectionString();
            SqlConnection myConnection = new SqlConnection(connectionString);

            myConnection.Open();

            string query = "SELECT id, name, price FROM Job where EnterpriceID = " + enterpriceID + " ORDER BY name";

            var dataAdapter = new SqlDataAdapter(query, myConnection);
            var commandBuilder = new SqlCommandBuilder(dataAdapter);

            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            myConnection.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int enterID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            updateJobs(enterID);
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int jobID = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value);

            string connectionString = GetConnectionString();
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            string query = "SELECT name, dscription, price FROM Job where id = " + jobID;

            SqlCommand command = new SqlCommand(query, myConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader[0].ToString(); ;
                textBox2.Text = reader[1].ToString(); ;
                textBox3.Text = reader[2].ToString(); ;
            }

            myConnection.Close();
        }
    }
}
