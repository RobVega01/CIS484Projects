using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace CIS484Projects
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UserLoginNavBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("1-LoginPage.aspx");
        }

        protected void userRegBtn_Click(object sender, EventArgs e)
        {
            //1. Validates that no text fields are empty (except maybe organization)
            if (firstTextbox.Text.Equals("") ||
                lastTextbox.Text.Equals("") ||
                emailTextbox.Text.Equals("") ||
                passTextbox.Text.Equals("") ||
                orgTextbox.Text.Equals(""))
            {
                emptyErrorLbl.Text = "No text fields can be empty.";
            } 
            else {
                //2. Validates that email is atomic
                if (!atomicEmail(HttpUtility.HtmlEncode(emailTextbox.Text)))
                {
                    nonAtomicEmailErrorLbl.Text = "This email is already in use.";
                } 
                else {
                    //3. Finds highest userID
                    int newUserID = highestUserID();
                    //4. Checks Organizations table for existing organization, or makes a new one with a new ID
                    int newOrgID = existingOrg(orgTextbox.Text);
                    //5. Registers new user in AUTH DB
                    SqlConnection authConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString);
                    SqlConnection lab3Connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["lab3"].ConnectionString);
                    
                    authConnection.Open();
                    lab3Connection.Open();

                    //Encrypting and inserting critical user information into AUTH DB
                    String authInsert = "INSERT INTO UserCritical (userID, email, hashPass) VALUES (@userID, @email, @hashPass)";
                    SqlCommand authCommand = new SqlCommand(authInsert, authConnection);
                    authCommand.Parameters.AddWithValue("userID", newUserID);
                    authCommand.Parameters.AddWithValue("email", HttpUtility.HtmlEncode(emailTextbox.Text));
                    authCommand.Parameters.AddWithValue("hashPass", PasswordHash.HashPassword(HttpUtility.HtmlEncode(passTextbox.Text)));
                    authCommand.ExecuteNonQuery();

                    //Inserting noncritical user information into Lab3 DB
                    String lab3Insert = "INSERT INTO UserNonCrit (userID, userFirst, userLast, orgID) VALUES (@userID, @userFirst, @userLast, @orgID)";
                    SqlCommand lab3Command = new SqlCommand(lab3Insert, lab3Connection);
                    lab3Command.Parameters.AddWithValue("userID", newUserID);
                    lab3Command.Parameters.AddWithValue("userFirst", HttpUtility.HtmlEncode(firstTextbox.Text));
                    lab3Command.Parameters.AddWithValue("userLast", HttpUtility.HtmlEncode(lastTextbox.Text));
                    lab3Command.Parameters.AddWithValue("orgID", newOrgID);
                    lab3Command.ExecuteNonQuery();

                    regSuccessLbl.Text = "Registration Successful!";
                }
            }
        }

        //3. Method to find highest userID
        protected int highestUserID() {
            SqlConnection sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString);
            sqlConnection.Open();
            String query = "SELECT MAX(userID) FROM UserCritical";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            int highest = -1;
            if (sqlCommand.ExecuteScalar().ToString() == null)
            {
                highest = 0;
            }
            else {
                highest = Convert.ToInt32(sqlCommand.ExecuteScalar()) + 1;
            }
            
            return highest;
        }


        //2. Method to check if email is atomic; returns true if it is
        protected Boolean atomicEmail(String email) {
            Boolean tf = false;
            SqlConnection sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString);
            sqlConnection.Open();
            String query = "SELECT email from UserCritical WHERE email = @email";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("email", email);
            String tester = Convert.ToString(sqlCommand.ExecuteScalar());
            if (sqlCommand.ExecuteScalar() == null) {
                tf = true;
            }
            return tf;
        }


        //4. Method for checking for existing organization
        protected int existingOrg(String orgName) {
            int returnID = -1;
            SqlConnection sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab3"].ConnectionString);
            sqlConnection.Open();
            String query = "SELECT orgID FROM Organizations WHERE orgName = @orgName";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("orgName", orgName);
            if (sqlCommand.ExecuteScalar() == null) {
                //If the orgID is not found, returnID = (highest orgID) + 1
                String maxQuery = "SELECT MAX(orgID) FROM Organizations";
                SqlCommand sqlCommand1 = new SqlCommand(maxQuery, sqlConnection);
                if (sqlCommand1.ExecuteScalar() is DBNull)
                {
                    returnID = 0;
                }
                else {
                    returnID = Convert.ToInt32(sqlCommand1.ExecuteScalar()) + 1;
                }
                

                //Make a new entry to the Organizations table with new ID
                String insert = "INSERT INTO Organizations (orgID, orgName, orgBio) VALUES (@orgID, @orgName, '')";
                SqlCommand sqlCommand2 = new SqlCommand(insert, sqlConnection);
                sqlCommand2.Parameters.AddWithValue("orgID", returnID);
                sqlCommand2.Parameters.AddWithValue("orgName", orgName);
                sqlCommand2.ExecuteNonQuery();
            }
            else {
                //If the orgID is found
                returnID = Convert.ToInt32(sqlCommand.ExecuteScalar());
            }

            return returnID;
        }

    }
}
        ///DEVELOPERS NOTES
        ///
        ///Eventually, you'll need to add more validation for email address and users' names.
        ///Users' first and last names should not possess any numbers or special characters.
        ///Emails should possess an @ symbol somewhere within them.