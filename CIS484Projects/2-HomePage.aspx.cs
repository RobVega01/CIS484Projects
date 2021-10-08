using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using Microsoft.Win32;

namespace CIS484Projects
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
            // Continue to page
            }
            else
            {
                Response.Redirect("1-LoginPage.aspx");
            }

            SqlConnection sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab2"].ConnectionString);
            sqlConnection.Open();
            String firstQuery = "SELECT firstName FROM Users WHERE email = @SessionUser";
            String lastQuery = "SELECT lastName FROM Users WHERE email = @SessionUser";
            String passQuery = "SELECT password FROM Users WHERE email = @SessionUser";
            String emailQuery = "SELECT email FROM Users WHERE email = @SessionUser";
            String orgNameQuery = "SELECT orgName FROM Users u, Organizations o WHERE email = @SessionUser AND u.orgID = o.orgID";

            SqlCommand firstCommand = new SqlCommand(firstQuery, sqlConnection);
            firstCommand.Parameters.AddWithValue("SessionUser", Session["UserName"]);
            SqlCommand lastCommand = new SqlCommand(lastQuery, sqlConnection);
            lastCommand.Parameters.AddWithValue("SessionUser", Session["UserName"]);
            SqlCommand passCommand = new SqlCommand(passQuery, sqlConnection);
            passCommand.Parameters.AddWithValue("SessionUser", Session["UserName"]);
            SqlCommand emailCommand = new SqlCommand(emailQuery, sqlConnection);
            emailCommand.Parameters.AddWithValue("SessionUser", Session["UserName"]);
            SqlCommand orgCommand = new SqlCommand(orgNameQuery, sqlConnection);
            orgCommand.Parameters.AddWithValue("SessionUser", Session["UserName"]);

            ChangeFirstTextBox.Text = firstCommand.ExecuteScalar().ToString();
            ChangeLastTextBox.Text = lastCommand.ExecuteScalar().ToString();
            ChangePasswordTextBox.Text = passCommand.ExecuteScalar().ToString();
            ChangeEmailTextBox.Text = emailCommand.ExecuteScalar().ToString();
            ChangeOrgTextBox.Text = orgCommand.ExecuteScalar().ToString();
        }

        protected void navToAnalysisBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("4-AnalysisPage.aspx");
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session["UserName"] = null;
            Response.Redirect("1-LoginPage.aspx");
        }

        protected void navToTextsBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("3-ViewTextsPage.aspx");
        }

        protected void UpdateAllInfoBtn_Click(object sender, EventArgs e)
        {
            //Creating DB Connection
            SqlConnection myConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab2"].ConnectionString);
            myConnection.Open();

            //Checking for empty text fields
            if (ChangeFirstTextBox.Text.Equals("") || ChangeLastTextBox.Text.Equals("") || ChangePasswordTextBox.Text.Equals("") ||
                ChangeEmailTextBox.Text.Equals("") || ChangeOrgTextBox.Text.Equals("")) {
                ErrorEmptyLbl.Text = "To update user information, no text field may be empty.";
            } else {
                
                //Querying DB for new email 
                String emailQuery = "SELECT email FROM Users WHERE email=@email";
                SqlCommand emailCommand = new SqlCommand(emailQuery, myConnection);
                emailCommand.Parameters.AddWithValue("email", ChangeEmailTextBox.Text);
                String queryReturn = emailCommand.ExecuteScalar().ToString();

                //Querying DB for organization ID
                String orgQuery = "SELECT orgID FROM Organizations WHERE orgName = @orgName";
                SqlCommand orgCommand = new SqlCommand(orgQuery, myConnection);
                orgCommand.Parameters.AddWithValue("orgName", ChangeOrgTextBox.Text);
                String orgReturn = orgCommand.ExecuteScalar().ToString();
                int orgReturn2 = Convert.ToInt32(orgReturn);


                //If organization does not exist, make a new one with an atomic ID
                if (orgReturn.Equals("")) { 
                    //Generate a new OrganizationID
                    orgReturn2 = AtomicOrgID(ChangeOrgTextBox.Text);
                }

                //Checking if the inputted email is already in the DB
                if (ChangeEmailTextBox.Text.Equals(queryReturn)) {
                    ErrorBadEmailLbl.Text = "The inputted email is already associated with a user account.";
                } else {
                    String update = "UPDATE Users SET firstName = @newFirst, " +
                        "lastName = @newLast, " +
                        "password = @newPass, " +
                        "email = @newEmail, " +
                        "orgID = @newOrg " +
                        "WHERE email = @sessionEmail";
                    SqlCommand sqlCommand = new SqlCommand(update, myConnection);
                    sqlCommand.Parameters.AddWithValue("newFirst", ChangeFirstTextBox.Text);
                    sqlCommand.Parameters.AddWithValue("newLast", ChangeLastTextBox.Text);
                    sqlCommand.Parameters.AddWithValue("newPass", ChangePasswordTextBox.Text);
                    sqlCommand.Parameters.AddWithValue("newEmail", ChangeEmailTextBox.Text);
                    sqlCommand.Parameters.AddWithValue("newOrg", orgReturn2);
                    sqlCommand.Parameters.AddWithValue("sessionEmail", Session["UserName"]);

                }
            } 
        }
        //Takes a String for the Organization Name
        //Returns the existing organization's ID or makes a new ID
        protected int AtomicOrgID(String orgName) {
            SqlConnection myConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab2"].ConnectionString);
            myConnection.Open();
            String noOrgID = "SELECT MAX(orgID) FROM Organizations";
            SqlCommand sqlCommand = new SqlCommand(noOrgID, myConnection);
            int returnID = Convert.ToInt32(sqlCommand.ExecuteScalar().ToString());
            return returnID + 1;
        }

        protected void UpdateOrgBioBtn_Click(object sender, EventArgs e)
        {
            SqlConnection myConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab2"].ConnectionString);
            myConnection.Open();
            String bio = UpdateOrgBioTextBox.Text;
            if (bio.Equals(""))
            {
                UpdateOrgBioTextBox.Text = "Textbox must have text in it to update organization bio.";
            }
            else {
                String update = "UPDATE Organizations SET orgBio = @orgBio WHERE orgID = (SELECT orgID FROM Users WHERE email = @SessionUser)";
                SqlCommand sqlCommand = new SqlCommand(update, myConnection);
                sqlCommand.Parameters.AddWithValue("orgBio", bio);
                sqlCommand.Parameters.AddWithValue("sessionUser", Session["UserName"]);
            }
            
        }
    }
}