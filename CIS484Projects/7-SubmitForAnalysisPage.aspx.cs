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
    public partial class WebForm7 : System.Web.UI.Page
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
        }

        protected void NavToAnalysesBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("4-AnalysisPage.aspx");
        }

        protected void ReturnToHomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("2-HomePage.aspx");
        }

        protected void AnSubBtn_Click(object sender, EventArgs e)
        {
            SnubAnalysis(AnalysisDDL.SelectedItem.Text);
        }

        protected void SnubAnalysis(String storyName) {
            //initial declaration of variables that will be used in final SQL statement
            int analysisID = -1;
            String analysisName = "";
            int textID = -1;


            //1. Read storyName 
            String name = storyName;
            //2. Check for Analyses associated with storyName
            SqlConnection sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab3"].ConnectionString);
            sqlConnection.Open();
            String query = "SELECT analysisName FROM Analyses a, Texts t, Users u WHERE a.textID = t.textID AND t.textName = @textName AND u.email = @SessionName";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("textName", AnalysisDDL.SelectedItem.Text);
            sqlCommand.Parameters.AddWithValue("SessionName", Session["UserName"]);
            String result = sqlCommand.ExecuteScalar().ToString();
            ///NAME CREATION///
            //3-4. If no Analyses exist for storyName, start with Analysis0. If Analyses DO exist for storyName, read the last char(acter) of analysisName.
            if (result.Equals("")) {
                analysisName = "Analysis0";
            } else {
                //If analyses exist
                //Find number of rows in query result
                String countQuery = "SELECT COUNT(analysisName) FROM Analyses a, Texts t, Users u WHERE a.textID = t.textID AND t.textName = @textName AND u.email = @SessionName";
                SqlCommand analysisCount = new SqlCommand(countQuery, sqlConnection);
                analysisCount.Parameters.AddWithValue("textName", AnalysisDDL.SelectedItem.Text);
                analysisCount.Parameters.AddWithValue("SessionName", Session["UserName"]);
                int countResult = Convert.ToInt32(analysisCount.ExecuteScalar().ToString());
                analysisName = "Analysis" + countResult;
            }
            ///ID CREATION///
            //5. Create new AnalysisID based on MAX(analysisID) (only IDs associated with User) or make a new one if analysisID query result == null
            String idQuery = "SELECT MAX(analysisID) FROM Analyses";
            SqlCommand idCount = new SqlCommand(idQuery, sqlConnection);
            int id = -1;
            id = Convert.ToInt32(idCount.ExecuteScalar().ToString());
            if (id == -1)
            {
                analysisID = 0;
            } else {
                analysisID = (id + 1);
            }
            ///SNUB RESULTS CREATION///
            //6. Create basic String snub result
            String finalResult = "SNUB: this story has now been analyzed " + id + " times.";
            //7. Query for textID to add in INSERT statement
            String textIDQuery = "SELECT textID FROM Texts t, Users u WHERE textName = @textName AND u.email = @SessionName";
            SqlCommand textIDCommand = new SqlCommand(textIDQuery, sqlConnection);
            textIDCommand.Parameters.AddWithValue("textName", AnalysisDDL.SelectedItem.Text);
            textIDCommand.Parameters.AddWithValue("SessionName", Session["UserName"]);
            textID = Convert.ToInt32(textIDCommand.ExecuteScalar().ToString());
            //8. Create INSERT statement with the new analysisID, analysisName, and result
            String insert = "INSERT INTO Analyses (analysisID, analysisName, results, textID) VALUES (@anID, @anName, @results, @textID)";
            SqlCommand command = new SqlCommand(insert, sqlConnection);
            command.Parameters.AddWithValue("anID", analysisID);
            command.Parameters.AddWithValue("anName", analysisName);
            command.Parameters.AddWithValue("results", finalResult);
            command.Parameters.AddWithValue("textID", textID);
            command.ExecuteNonQuery();
        }
    }
}