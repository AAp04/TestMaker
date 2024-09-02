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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TestMaker
{
    public partial class UpdateDeleteTests : Form
    {
        OleDbConnection myOLEDBConnection;
        public int Id;

        public UpdateDeleteTests()
        {
            InitializeComponent();
        }

        private void UpdateDeleteTests_Load(object sender, EventArgs e)
        {
            myOLEDBConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb");

            BindComboBox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TeacherDashboard td2 = new TeacherDashboard();
            td2.Id = Id;
            td2.Show();

            this.Close();
        }

        private void BindComboBox()
        {
            try
            {
                string query = "SELECT TestName FROM Tests";

                myOLEDBConnection.Open();

                OleDbCommand command = myOLEDBConnection.CreateCommand();
                command.CommandText = query;

                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    // Set the ComboBox's DataSource to the DataTable
                    comboBox1.DataSource = dataTable;

                    // Set the DisplayMember to the column name that contains the values to display
                    comboBox1.DisplayMember = "TestName";
                }

                myOLEDBConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error binding ComboBox from the database: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "") {
                //load the data into fields 

                myOLEDBConnection.Open();

                OleDbTransaction transaction = myOLEDBConnection.BeginTransaction();

                OleDbCommand command = myOLEDBConnection.CreateCommand();
                command.Transaction = transaction;

                command.CommandText = $"SELECT * FROM Tests WHERE TestName = '{comboBox1.Text}'";

                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    string q1 = reader["Q1"].ToString();
                    string q2 = reader["Q2"].ToString();
                    string q3 = reader["Q3"].ToString();
                    string q4 = reader["Q4"].ToString();
                    string q5 = reader["Q5"].ToString();
                    string a1 = reader["A1"].ToString();
                    string a2 = reader["A2"].ToString();
                    string a3 = reader["A3"].ToString();
                    string a4 = reader["A4"].ToString();
                    string a5 = reader["A5"].ToString();

                    Q1.Text = q1;
                    Q2.Text = q2;
                    Q3.Text = q3;
                    Q4.Text = q4;
                    Q5.Text = q5;
                    A1.Text = a1;
                    A2.Text = a2;
                    A3.Text = a3;
                    A4.Text = a4;
                    A5.Text = a5;

                }
                transaction.Commit();
                myOLEDBConnection.Close();

            }
        }


        public void UpdateQuestionsAndAnswers(string testName, string q1, string q2, string q3, string q4, string q5,
                                      string a1, string a2, string a3, string a4, string a5)
        {
            try
            {
                myOLEDBConnection.Open();

                OleDbTransaction transaction = myOLEDBConnection.BeginTransaction();

                OleDbCommand command = myOLEDBConnection.CreateCommand();
                command.Transaction = transaction;

                // Interpolated string which can be used to concanate string values.
                command.CommandText = $"UPDATE Tests SET Q1 = @Q1, Q2 = @Q2, Q3 = @Q3, Q4 = @Q4, Q5 = @Q5, " +
                                                      $"A1 = @A1, A2 = @A2, A3 = @A3, A4 = @A4, A5 = @A5 " +
                                       $"WHERE TestName = @TestName";

                // Add parameters for the query
                command.Parameters.AddWithValue("@Q1", q1);
                command.Parameters.AddWithValue("@Q2", q2);
                command.Parameters.AddWithValue("@Q3", q3);
                command.Parameters.AddWithValue("@Q4", q4);
                command.Parameters.AddWithValue("@Q5", q5);

                command.Parameters.AddWithValue("@A1", a1);
                command.Parameters.AddWithValue("@A2", a2);
                command.Parameters.AddWithValue("@A3", a3);
                command.Parameters.AddWithValue("@A4", a4);
                command.Parameters.AddWithValue("@A5", a5);

                command.Parameters.AddWithValue("@TestName", testName);

                // Execute the query
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    transaction.Commit();
                    MessageBox.Show($"Updated {rowsAffected} rows for TestName '{testName}'.");
                }
                else
                {
                    MessageBox.Show($"No rows updated for TestName '{testName}'.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating questions and answers: " + ex.Message);
            }
           
            myOLEDBConnection.Close();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (Q1.Text != "" && Q2.Text != "" && Q3.Text != "" && Q4.Text != "" && Q5.Text != "")
            {
                UpdateQuestionsAndAnswers(comboBox1.Text, Q1.Text, Q2.Text, Q3.Text,  Q4.Text,
                Q5.Text, A1.Text, A2.Text, A3.Text, A4.Text, A5.Text);
            }

        }

        public void DeleteTest(string testName)
        {

            try
            {
                myOLEDBConnection.Open();

                OleDbTransaction transaction = myOLEDBConnection.BeginTransaction();

                OleDbCommand command = myOLEDBConnection.CreateCommand();
                command.Transaction = transaction;

                command.CommandText = $"DELETE FROM Tests WHERE TestName = @TestName";

                command.Parameters.AddWithValue("@TestName", testName);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    transaction.Commit();
                    MessageBox.Show($"Deleted {rowsAffected} row(s) for TestName '{testName}'.");
                }
                else
                {
                    MessageBox.Show($"No rows deleted for TestName '{testName}'.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting test: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Q1.Text != "" && Q2.Text != "" && Q3.Text != "" && Q4.Text != "" && Q5.Text != "")
            {
                //delete button
                DeleteTest(comboBox1.Text);
            }
        }
    }
}
