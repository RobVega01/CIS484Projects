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
    public partial class WebForm6 : System.Web.UI.Page
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

        protected void navToTextsBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("3-ViewTextsPage.aspx");
        }

        protected void ReturnToHomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("2-HomePage.aspx");
        }

        //Button to Commit Text Edits to DB
        protected void UpdateTextBtn_Click(object sender, EventArgs e)
        {
            SqlConnection myConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab3"].ConnectionString);
            myConnection.Open();
            String query = "SELECT textID FROM Texts WHERE textName = @dropDownName";
            SqlCommand sql = new SqlCommand(query, myConnection);
            sql.Parameters.AddWithValue("dropDownName", EditDropDownList.SelectedItem.Text);
            int ID = Convert.ToInt32(sql.ExecuteScalar().ToString());
                

            if (TitleTextBox.Text.Equals("") || ContentTextBox.Text.Equals("")) {
                ContentTextBox.Text = "The title and content of a story cannot be empty.";
            } else {
                String update = "UPDATE Texts SET textName = @textName, textContent = @content WHERE textID = @ID";
                SqlCommand sqlCommand = new SqlCommand(update, myConnection);
                sqlCommand.Parameters.AddWithValue("textName", TitleTextBox.Text);
                sqlCommand.Parameters.AddWithValue("content", ContentTextBox.Text);
                sqlCommand.Parameters.AddWithValue("ID", ID);
                sqlCommand.ExecuteNonQuery();
            }
            
        }
        
        protected void EditDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection myConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab3"].ConnectionString);
            myConnection.Open();
            String query = "SELECT textContent FROM Texts WHERE textName = @textName";
            SqlCommand sqlCommand = new SqlCommand(query, myConnection);
            sqlCommand.Parameters.AddWithValue("textName", EditDropDownList.SelectedItem.Text);
            String result = sqlCommand.ExecuteScalar().ToString();

            TitleTextBox.Text = EditDropDownList.SelectedItem.Text;
            ContentTextBox.Text = result;
        }

        
    }
}