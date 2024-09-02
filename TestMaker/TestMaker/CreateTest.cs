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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.Common;

namespace TestMaker
{
    public partial class CreateTest : Form
    {
        OleDbConnection myOLEDBConnection;
        public int Id;
        public CreateTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TestName.Text != "" && Q1.Text != "" && A1.Text != "" && Q2.Text != "" && A2.Text != "" &&
                Q3.Text != "" && A3.Text != "" && Q4.Text != "" && A4.Text != "" &&
                Q5.Text != "" && A5.Text != "")
            {
                // Create a TestItem object with the data from the form
                Tests newTest = new Tests(TestName.Text, Q1.Text, A1.Text,
                                                Q2.Text, A2.Text,
                                                Q3.Text, A3.Text,
                                                Q4.Text, A4.Text,
                                                Q5.Text, A5.Text);

                myOLEDBConnection.Open();
                OleDbTransaction transaction = myOLEDBConnection.BeginTransaction();
                OleDbCommand command = myOLEDBConnection.CreateCommand();
                command.Transaction = transaction;

                command.CommandText = $"INSERT INTO Tests ([TestName], [Q1], [Q2], [Q3], [Q4], [Q5], [A1], [A2], [A3], [A4], [A5]) " +
                                      $"VALUES ('{newTest.TestName}', '{Q1.Text}', '{Q2.Text}', " +
                                              $"'{Q3.Text}', '{Q4.Text}', '{Q5.Text}', '{A1.Text}', " +
                                              $"'{A2.Text}', '{A3.Text}', '{A4.Text}', '{A5.Text}')";

                command.ExecuteNonQuery();

                transaction.Commit();

                myOLEDBConnection.Close();
                MessageBox.Show("TEST CREATED SUCCESSFULLY!!!");

                // go back to main teacher dashboard
                TeacherDashboard td = new TeacherDashboard();
                td.Id = Id;
                td.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Please fill in all the question-answer pairs.");
            }

            
        }

        private void CreateTest_Load(object sender, EventArgs e)
        {
            myOLEDBConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TeacherDashboard td2 = new TeacherDashboard();
            td2.Id = Id;
            td2.Show();

            this.Close();
        }

        private void Q5_TextChanged(object sender, EventArgs e)
        {

        }

        private void A4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Q4_TextChanged(object sender, EventArgs e)
        {

        }

        private void A3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Q3_TextChanged(object sender, EventArgs e)
        {

        }

        private void A2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Q2_TextChanged(object sender, EventArgs e)
        {

        }

        private void A1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Q1_TextChanged(object sender, EventArgs e)
        {

        }

        private void A5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
