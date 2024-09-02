using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestMaker
{
    public partial class Result : Form
    {
        public int Id;
        OleDbConnection myOLEDBConnection;

        public Result()
        {
            InitializeComponent();
        }

        private void Result_Load(object sender, EventArgs e)
        {
            myOLEDBConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb");


            try
            {
                myOLEDBConnection.Open();

                string query = "SELECT * FROM Results";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, myOLEDBConnection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error binding DataGridView to Result table: " + ex.Message);
            }
            finally
            {
                myOLEDBConnection.Close();
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            TeacherDashboard td2 = new TeacherDashboard();
            td2.Id = Id;
            td2.Show();

            this.Close();
        }
    }
}
