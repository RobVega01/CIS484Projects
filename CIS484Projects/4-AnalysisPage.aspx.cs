using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;


namespace CIS484Projects
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
             //Continues to load page
            }
            else
            {
                Response.Redirect("1-LoginPage.aspx");
            }

            if (!Page.IsPostBack) {
                ListItem list = new ListItem();
                list.Text = "Choose";
                list.Value = string.Empty;

                TextDropDownList.Items.Insert(0, list);
                AnalysisDropDownList.Items.Insert(0, list);
            }

        }

        protected void NavToUploadBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("5-UploadPage.aspx");
        }

        protected void NavToHomePageBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("2-HomePage.aspx");
        }

        protected void NavToAnalysisSubmitBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("7-SubmitForAnalysisPage.aspx");
        }

        protected void TextDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab2"].ConnectionString);
            sqlConnection.Open();

            
            String ddl2Query = "SELECT analysisID, analysisName FROM Analyses a, Texts t, Users u WHERE a.textID = t.textID AND t.userID = u.userID AND t.textName = @DropDownIndex AND u.email = @sessionUser";
            SqlCommand sqlCommand = new SqlCommand(ddl2Query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("DropDownIndex", TextDropDownList.SelectedItem.Text);
            sqlCommand.Parameters.AddWithValue("sessionUser", Session["UserName"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            AnalysisDropDownList.DataSource = dt;
            AnalysisDropDownList.DataBind();
            AnalysisDropDownList.DataValueField = "analysisID";
            AnalysisDropDownList.DataTextField = "analysisName";

            

        }

        protected void AnalysisDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection lab3Connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab3"].ConnectionString);
            SqlConnection authConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString);
            lab3Connection.Open();
            authConnection.Open();

            String query = "SELECT results FROM Analyses a, Texts t, UserNonCrit u " +
                "WHERE a.textID = t.textID " +
                "AND t.userID = u.userID " +
                "AND t.textName = @textName " +
                "AND u.email = @sessionUser " +
                "AND a.analysisName = @chosenAnalysis";
            SqlCommand sql = new SqlCommand(query, lab3Connection);
            sql.Parameters.AddWithValue("textName", TextDropDownList.SelectedItem.Text);
            sql.Parameters.AddWithValue("sessionUser", Session["UserName"]);
            sql.Parameters.AddWithValue("chosenAnalysis", AnalysisDropDownList.SelectedItem.Text);
            if (AnalysisDropDownList.SelectedItem.Text.Equals("Choose")) {
                AnalysisDisplayTextBox.Text = "No analysis associated with this option.";
            } else {
                AnalysisDisplayTextBox.Text = sql.ExecuteScalar().ToString();
            }
        }
    }
}
