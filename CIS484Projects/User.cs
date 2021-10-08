using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS484Projects
{
    public class User
    {
        /// Base Values to be put into a User Class
        private String first = "";
        private String last = "";
        private String email = "";
        private String orgReturn = "";
        public User()
        {
            ///Default Constructor for User Class. Function-Testing Setter for SQL DB.
            ///Base Values: ("Bobby","Basic","basbo@dukes.jmu.edu","James Madison University").
            ///Returns a User to be read into the SQL DB's User Table.
            first = "Bobby";
            last = "Basic";
            email = "basbo@dukes.jmu.edu";
            orgReturn = "James Madison University";
        }

        public User(String firstName, String lastName, String emailAddress, String orgAffiliation)
        {
            ///Overloaded Constructer for User Class. Functional Setter for SQL DB.
            ///Sets Values of new DB item to (firstName, lastName, emailAddress, orgAffiliation).
            ///Also returns a UserID # for a primary key.
            ///Returns a User to be read into the SQL DB's User Table.
            first = firstName;
            last = lastName;
            email = emailAddress;
            orgReturn = orgAffiliation;
        }
        
    }
}