using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestMaker
{
    public partial class TeacherDashboard : Form
    {
        public int Id;
        OleDbConnection myOLEDBConnection;

        public TeacherDashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateTest ct = new CreateTest();
            ct.Id = Id;
            ct.Show();

            this.Close();

        }

        private void TeacherDashboard_Load(object sender, EventArgs e)
        {
            myOLEDBConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb");
            label3.Text = Id.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateDeleteTests udt = new UpdateDeleteTests();
            udt.Id = Id;
            udt.Show();

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Result r = new Result();
            r.Id = Id;
            r.Show();

            this.Close();
        }
    }
}
