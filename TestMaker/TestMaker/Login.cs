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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace TestMaker
{
    public partial class Login : Form
    {
        OleDbConnection myOLEDBConnection;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            myOLEDBConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usernames = username.Text;
            string password = userpassword.Text;
            string usertype = comboBox1.Text;


            if (usernames != "" && password != "")
            {
                string query = "SELECT COUNT(*) FROM Users WHERE username = '" + usernames +"' AND [password] = '" + password +"' AND [usertype] = '" + usertype +"'";

                myOLEDBConnection.Open();
                OleDbCommand command = myOLEDBConnection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("username", usernames);
                command.Parameters.AddWithValue("password", password);
                command.Parameters.AddWithValue("usertype", usertype);

                int count = Convert.ToInt32(command.ExecuteScalar());
                myOLEDBConnection.Close();

                if (count > 0 && usertype == "Student")
                {
                    MessageBox.Show("Student Login successfully!");
                    string query1 = "SELECT Id FROM Users WHERE username = '" + usernames + "' AND [password] = '" + password + "' AND [usertype] = '" + usertype + "'";

                    myOLEDBConnection.Open();

                    OleDbCommand command1 = myOLEDBConnection.CreateCommand();
                    command1.CommandText = query1;
                    command1.Parameters.AddWithValue("username", usernames);
                    command1.Parameters.AddWithValue("password", password);
                    command1.Parameters.AddWithValue("usertype", usertype);

                    int Id = Convert.ToInt32(command1.ExecuteScalar());
                    myOLEDBConnection.Close();

                    StudentDashboard studentDashboard = new StudentDashboard();
                    
                    studentDashboard.Id = Id;

                    studentDashboard.Show();
                    this.Hide();

                }
                else if (count > 0 && usertype == "Teacher") {
                    MessageBox.Show("Teacher Login successfully!");

                    string query2 = "SELECT Id FROM Users WHERE username = '" + usernames + "' AND [password] = '" + password + "' AND [usertype] = '" + usertype + "'";

                    myOLEDBConnection.Open();

                    OleDbCommand command2 = myOLEDBConnection.CreateCommand();
                    command2.CommandText = query2;
                    command2.Parameters.AddWithValue("username", usernames);
                    command2.Parameters.AddWithValue("password", password);
                    command2.Parameters.AddWithValue("usertype", usertype);

                    int Id = Convert.ToInt32(command2.ExecuteScalar());
                    myOLEDBConnection.Close();
                    TeacherDashboard teacherDashboard = new TeacherDashboard();
                    teacherDashboard.Id = Id;
                    teacherDashboard.Show();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("User Not Found!");
                }
            }
        }
    }
}
