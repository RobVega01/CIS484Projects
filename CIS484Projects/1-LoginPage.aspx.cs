using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;



namespace CIS484Projects
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            //Referencing ConnectionString in Web.config
            SqlConnection sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString);
            sqlConnection.Open();

            //Searching for inputted email in database via parameterized query
            String emailQuery = "SELECT email FROM Users WHERE email = @email";
            SqlCommand emailCommand = new SqlCommand(emailQuery, sqlConnection);
            emailCommand.Parameters.AddWithValue("email", EmailForLoginTextBox.Text);
            String email = emailCommand.ExecuteScalar().ToString();

            //Testing inputted password against associated email via parameterized query
            String passQuery = "SELECT password FROM Users WHERE email = @email";
            SqlCommand passwordCommand = new SqlCommand(passQuery, sqlConnection);
            passwordCommand.Parameters.AddWithValue("email", EmailForLoginTextBox.Text);
            String DBpassword = passwordCommand.ExecuteScalar().ToString();

            //Closing DB connection
            sqlConnection.Close();

            if (email.Length > 0 && PasswordForLoginTextBox.Text.Equals(DBpassword))
            {
                Session["UserName"] = EmailForLoginTextBox.Text;
                Response.Redirect("2-HomePage.aspx");
            }
            else {
                ResultLabel.Text = "This email is not in the database or the password is incorrect.";
            }
        }

        protected void registrationNavButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("1a-UserRegPage.aspx");
        }

        protected void AdminLoginBtn_Click(object sender, EventArgs e)
        {
            EmailForLoginTextBox.Text = "admin";
            PasswordForLoginTextBox.Text = "password";
        }
    }
}