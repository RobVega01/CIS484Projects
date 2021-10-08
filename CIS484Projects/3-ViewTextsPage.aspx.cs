using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace CIS484Projects
{
    public partial class WebForm5 : System.Web.UI.Page
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

        protected void navToUploadBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("5-UploadPage.aspx");
        }

        protected void navToHomepageBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("2-HomePage.aspx");
        }

        protected void navToEditingBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("6-EditTextsPage.aspx");
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab3"].ConnectionString);
            sqlConnection.Open();
            String query = "SELECT textContent FROM Texts WHERE textName = @textName";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("textName", DropDownList1.SelectedItem.Text);
            
            TextReaderContent.Text = sqlCommand.ExecuteScalar().ToString();
        }
    }
}