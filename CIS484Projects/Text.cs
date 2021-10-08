using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;

namespace CIS484Projects
{
    public class Text
    {
        /// Base values to be in a text file
        private int textID = 0;
        private int userID = 0;
        private String textTitle = "";
        private String textContent = "";

        /// Default Constructor for Temporary Text Object
        public Text() {
            textID = 1560;
            userID = 2421;
            textTitle = "SQL to C# Practice";
            textContent = "This is a practice document for" +
                "learning the connection between a SQL database and a C# UI.";
        }

        /// Overloaded Constructor for Temporary Text Object
        public Text(int newTextID, int newUserID, String newTextTitle, String newTextContent) {
            textID = newTextID;
            userID = newUserID;
            textTitle = newTextTitle;
            textContent = newTextContent;
        }
    }
}