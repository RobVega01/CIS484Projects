using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;

namespace CIS484Projects
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
            //Continue loading page
            }
            else 
            {
                Response.Redirect("1-LoginPage.aspx");
            }
        }
        protected void uploadNewTextBtn_Click(object sender, EventArgs e)
        {
            int errorCounter = 0;
            int userID = EmailToUserID(Session["UserName"].ToString());
            
            if (nameUpload.Text.Length == 0 || textContentUpload.Text.Length == 0) {
                //Communicate to User that text fields cannot be empty
                ContentErrorLabel.Text = "One of the associated fields is empty.";
                errorCounter++;

            } if (AtomicTitle(nameUpload.Text) == false) {
                //Communicate to the User that the Title They Entered Already Exists
                NameErrorLabel.Text = "This title has already been used. Enter another title.";
                errorCounter++;

            } if (errorCounter == 0) {
                //Adds a New Text to the Texts Table if all Validations Are Passed
                SqlConnection myConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab3"].ConnectionString);
                myConnection.Open();

                int textID = HighestTextID();
                String textQuery = "INSERT INTO Texts (textID, userID, textName, textContent) VALUES (@textID, @userID, @textName, @textContent)";
                SqlCommand myCommand = new SqlCommand(textQuery, myConnection);
                myCommand.Parameters.AddWithValue("textID", textID);
                myCommand.Parameters.AddWithValue("userID", userID);
                myCommand.Parameters.AddWithValue("textName", nameUpload.Text);
                myCommand.Parameters.AddWithValue("textContent", textContentUpload.Text);
                myCommand.ExecuteNonQuery();
            }
        }
        protected void navToTextsBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("3-ViewTextsPage.aspx");
        }

        protected void navToHomepageBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("2-HomePage.aspx");
        }

        protected void navToAnalysisBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("4-AnalysisPage.aspx");
        }

        protected int EmailToUserID(String email)
        { //Translates a Given Email Into the UserID if it Exists

            //TEMPORARY DB CONNECTION OUTSIDE WEBCONFIG
            SqlConnection myConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab3"].ConnectionString);
            myConnection.Open();

            //Querying SQL Database
            String query = "SELECT userID FROM Users WHERE email = @email";
            SqlCommand myCommand = new SqlCommand(query, myConnection);
            myCommand.Parameters.AddWithValue("email", email);
            myCommand.ExecuteNonQuery();

            return Convert.ToInt32(myCommand.ExecuteScalar().ToString());
        }
        protected Boolean AtomicTitle(String title) { //Returns False if the Text Title is Already in Existence 

            SqlConnection myConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab3"].ConnectionString);
            myConnection.Open();

            //Check Database for Title
            String query = "SELECT textName FROM Texts WHERE textName = @textName";
            SqlCommand myCommand = new SqlCommand(query, myConnection);
            myCommand.Parameters.AddWithValue("textName", title);
            myCommand.ExecuteNonQuery();

            //Saving Query Result
            SqlCommand atomicTitle = myConnection.CreateCommand();
            atomicTitle.CommandText = query;
            String titleIsGood;
            if (atomicTitle.ExecuteScalar() == null) {
                 titleIsGood = "";
            } else
            {
                 titleIsGood = atomicTitle.ExecuteScalar().ToString();
            }
            
            if (!titleIsGood.Equals("")) {
                //Return False if the Entered Title is Found
                return false;
            }
            //Return True if the Entered Title is Not Found
            return true;
        }

        protected int HighestTextID() {

            SqlConnection myConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab3"].ConnectionString);
            myConnection.Open();

            //Querying the Text table for the highest textID value
            int newTextID = 999999999;
            String query = "SELECT MAX(textID) FROM Texts";
            SqlCommand myCommand = new SqlCommand(query, myConnection);

            SqlCommand CmdHighest = myConnection.CreateCommand();
            CmdHighest.CommandText = query;
            String x = CmdHighest.ExecuteScalar().ToString();
            
            if (x.Equals(""))
            {
                newTextID = 0;
            }
            else
            {
                newTextID = (int)CmdHighest.ExecuteScalar() + 1;
                //Cast the highest possible UserID value as an integer
            }
            return newTextID;
        }
        
        protected void fileReaderBtn_Click(object sender, EventArgs e)
        {
            nameUpload.Text = TextFileUpload.FileName;
            StreamReader reader = new StreamReader(TextFileUpload.PostedFile.InputStream);
            textContentUpload.Text = reader.ReadToEnd();
        }
    }
}