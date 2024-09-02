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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TestMaker
{
    public partial class Register : Form
    {
        OleDbConnection myOLEDBConnection;
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            myOLEDBConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (username.Text != "" && useremail.Text != "" && userpassword.Text != "")
            {

                User newUser = new User(username.Text, useremail.Text, userpassword.Text, comboBox1.Text);

                myOLEDBConnection.Open();
                OleDbCommand command = myOLEDBConnection.CreateCommand();
                command.CommandText = "INSERT INTO Users ([username], [useremail], [password], [usertype]) VALUES (?, ?, ?, ?)";
                command.Parameters.AddWithValue("username", newUser.username);
                command.Parameters.AddWithValue("useremail", newUser.useremail);
                command.Parameters.AddWithValue("password", newUser.password);
                command.Parameters.AddWithValue("usertype", newUser.usertype);

                command.ExecuteNonQuery();
                myOLEDBConnection.Close();
                MessageBox.Show("REGISTER SUCCESSFULLY!!!");

            }
            else
            {
                MessageBox.Show("SOME FIELDS ARE EMPTY!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();

            this.Hide();
        }
    }
}
