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
namespace ACT1A_CRUD
{
    public partial class Form2 : Form
    {
        private MySqlConnection connection;
        public Form2()
        {
            InitializeComponent();
            connection = new MySqlConnection("server=localhost;database=bermudo;username=root;password=;");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            Form1 loginpage = new Form1();
            loginpage.Show();
            this.Hide();
        }

       

        private void btnRegisterAccount_Click_1(object sender, EventArgs e)
        {
            //declare variable for inputs
            string name = txtName.Text;
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            //Check if  name usename and password are not empty
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter both name,username and password");
            }
            try
            {
                //Register Account Code 
                connection.Open();
                string registerquery = "INSERT INTO users (name, username, password) VALUES (@name, @username, @password)";
                MySqlCommand command = new MySqlCommand(registerquery, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                //Get Row Affected
                int rowAffected = command.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    MessageBox.Show("Register Successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to Register Account!");
                }

            }
            catch (Exception ex)
            {
                //Display any error if occupied
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // close connection wheter there is a error or not
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
                txtName.Clear();
                txtUsername.Clear();
                txtPassword.Clear();
            }

        }
    }
} 

