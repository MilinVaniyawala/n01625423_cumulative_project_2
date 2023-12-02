﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;

namespace n01625423_cumulative_project_2.Models
{
    /// <summary>
    /// A class which connects to your MySQL database
    /// </summary>
    public class SchoolDbContext
    { 
        /// ReadOnly Properties 
        /// Only the SchoolDbContext class can use it

        private static string User { get { return "root"; } }
        private static string Password { get { return ""; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        /// ConnectionString is a series of credentials used to connect to the database.
        protected static string ConnectionString
        {
            get
            {
                /// Convert Zero Datetime is a setting that will interpret a 0000-00-00 as null
                /// This makes it easier for C# to convert to a proper DateTime type
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";
            }
        }

        /// This is the method we actually use to get the database!
        /// <summary>
        /// Returns a connection to the school database.
        /// </summary>
        /// <example>
        /// private SchoolDbContext School = new SchoolDbContext();
        /// MySqlConnection Conn = School.AccessDatabase();
        /// </example>
        /// <returns> A MySqlConnection Object </returns>

        public MySqlConnection AccessDatabase()
        {
            /// MySqlConnection Class to create an object
            /// the object is a specific connection to our school database on port 3306 of localhost
            return new MySqlConnection(ConnectionString);
        }
    }
}