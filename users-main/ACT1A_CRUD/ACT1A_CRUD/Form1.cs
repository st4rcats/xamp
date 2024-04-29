using MySql.Data.MySqlClient;
namespace ACT1A_CRUD
{
    public partial class Form1 : Form
    {
        //Declare MySqlConnection for handling database connection
        private MySqlConnection connection;
        public Form1()
        {
            InitializeComponent();
            connection = new MySqlConnection("server=localhost;database=bermudo;username=root;password=;");
        }

        private void chbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chbShowPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            //Sanitzied data to remove white-space
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            //Check if usename and password are not empty
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password");
            }
            try
            {
                //open the connection string
                connection.Open();
                //create a string that will handle query
                string loginquery = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";

                //Execute the command loginquery
                MySqlCommand command = new MySqlCommand(loginquery, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                //Get row count
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count > 0)

                {
                    //MessageBox.Show("Incorrect username and password");
                    Admin admipage = new Admin();
                    admipage.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
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
            }



        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void LinkSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 signuppage = new Form2();
            signuppage.Show();
            this.Hide();
        }
    }
}
