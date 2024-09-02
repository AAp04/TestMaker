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

namespace TestMaker
{
    public partial class StudentDashboard : Form
    {
        OleDbConnection myOLEDBConnection;

        public int Id;


        public StudentDashboard()
        {
            InitializeComponent();
        }

        private void StudentDashboard_Load(object sender, EventArgs e)
        {
            myOLEDBConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb");

            label3.Text = Id.ToString();

            // Bind ComboBox
            BindComboBox();

        }

        private void LoadQuestionsFromDatabase(string testName)
        {
            try
            {
                string query = "SELECT Q1, Q2, Q3, Q4, Q5 FROM Tests WHERE TestName = ?";
                    
                myOLEDBConnection.Open();

                OleDbCommand command = myOLEDBConnection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("TestName", testName);

                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Display questions on the form dynamically
                        int y = 170; // Initial y-coordinate for label controls
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Label questionLabel = new Label();
                            questionLabel.Text = reader[i].ToString();
                            questionLabel.AutoSize = true;
                            questionLabel.Location = new System.Drawing.Point(20, y);
                            questionLabel.Tag = "QuestionLabel";
                            this.Controls.Add(questionLabel);

                            // Adjust y-coordinate for next label
                            y += questionLabel.Height + 20; // Add some spacing between labels
                        }
                    }
                    else
                    {
                        MessageBox.Show("No test found with the specified name.");
                    }
                }
                myOLEDBConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading questions from the database: " + ex.Message);
            }
        }


        private void BindComboBox ()
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


        private void ClearQuestionLabels()
        {
            // Remove existing question labels with the specified tag from the form
            foreach (Control control in this.Controls)
            {
                if (control is Label && control.Tag != null && control.Tag.ToString() == "QuestionLabel")
                {
                    this.Controls.Remove(control);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text != "")
            {
                ClearQuestionLabels();
                LoadQuestionsFromDatabase(comboBox1.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Students studentTest = new Students(Id.ToString(), comboBox1.Text, UA1.Text, UA2.Text, UA3.Text, UA4.Text, UA5.Text); ;
            myOLEDBConnection.Open();
            OleDbTransaction transaction = myOLEDBConnection.BeginTransaction();
            OleDbCommand commandInsert = myOLEDBConnection.CreateCommand();
            commandInsert.Transaction = transaction;

            commandInsert.CommandText = $"INSERT INTO Students ([StudentId], [TestName], [UA1], [UA2], [UA3], [UA4], [UA5]) " +
                                  $"VALUES ('{studentTest.StudentId}', '{studentTest.TestName}', '{studentTest.UA1}', " +
                                          $"'{studentTest.UA2}', '{studentTest.UA3}', '{studentTest.UA4}', '{studentTest.UA5}')";

            commandInsert.ExecuteNonQuery();

            transaction.Commit();

            myOLEDBConnection.Close();
            MessageBox.Show("Successfully Submitted!");

        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                // Open the database connections
                myOLEDBConnection.Open();
                

                // Create commands
                OleDbCommand command = myOLEDBConnection.CreateCommand();
                OleDbCommand commandInsert = myOLEDBConnection.CreateCommand();

                // Set the SELECT query for retrieving rows from the Tests table
                command.CommandText = $"SELECT * FROM Tests WHERE TestName = @TestName";

                // Set parameter for the query
                command.Parameters.AddWithValue("@TestName", comboBox1.Text);

                // Execute the query and store the results in a data reader
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    // Check if a row is found in the Tests table
                    if (reader.Read())
                    {
                        int matchedAnswersCount = 0; // Counter for matched answers

                        // Retrieve values of A1 to A5 from the Tests table
                        string a1Tests = reader["A1"].ToString();
                        string a2Tests = reader["A2"].ToString();
                        string a3Tests = reader["A3"].ToString();
                        string a4Tests = reader["A4"].ToString();
                        string a5Tests = reader["A5"].ToString();

                        // Set the SELECT query for retrieving rows from the Students table
                        commandInsert.CommandText = $"SELECT * FROM Students WHERE TestName = @TestName";
                    
                        commandInsert.Parameters.AddWithValue("@TestName", comboBox1.Text);
                        // Execute the query and store the results in a data reader
                        using (OleDbDataReader readerInsert = commandInsert.ExecuteReader())
                        {
                            // Check if a row is found in the Students table
                            if (readerInsert.Read())
                            {
                                // Retrieve values of UA1 to UA5 from the Students table
                                string ua1Students = readerInsert["UA1"].ToString();
                                string ua2Students = readerInsert["UA2"].ToString();
                                string ua3Students = readerInsert["UA3"].ToString();
                                string ua4Students = readerInsert["UA4"].ToString();
                                string ua5Students = readerInsert["UA5"].ToString();

                                // Compare each answer and increment counter for matched answers
                                if (a1Tests == ua1Students)
                                {
                                    matchedAnswersCount++;
                                }
                                if (a2Tests == ua2Students)
                                {
                                    matchedAnswersCount++;
                                }
                                if (a3Tests == ua3Students)
                                {
                                    matchedAnswersCount++;
                                }
                                if (a4Tests == ua4Students)
                                {
                                    matchedAnswersCount++;
                                }
                                if (a5Tests == ua5Students)
                                {
                                    matchedAnswersCount++;
                                }

                                // Print the count of matched answers
                                MessageBox.Show($"Number of matched answers: {matchedAnswersCount}");

                                //insert into Result table:
                                InsertResult(Id.ToString(), comboBox1.Text, matchedAnswersCount.ToString());

                            }
                            else
                            {
                                MessageBox.Show("No corresponding row found in the Students table.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No corresponding row found in the Tests table.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
               MessageBox.Show("Error comparing data between Tests and Students tables: " + ex.Message);
            }
            finally
            {
                // Close the database connections
                myOLEDBConnection.Close();
            }


        }


        public void InsertResult(string studentId, string testName, string result)
        {
            try
            {

                OleDbCommand command = myOLEDBConnection.CreateCommand();

                command.CommandText = $"INSERT INTO Results ([StudentId], [TestName], [Result]) VALUES ('{studentId}', '{testName}', '{result}')";

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Result inserted successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to insert result.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting result: " + ex.Message);
            }
            
        }


    }
}
